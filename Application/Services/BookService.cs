using Application;
using Application.DTOs.Book;
using Application.Exceptions;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using LibraryManagementAPI.Models;


namespace LibraryManagementAPI.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ICategoryRepository _categoryRepository;

        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository, ICategoryRepository categoryRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<ApiResponse<IEnumerable<BookDetailsDto>>> GetAll(int pageSize = 3, int pageNumber = 1, 
            int? AuthorId = null, int? CategoryId = null, string? searchByTitle = null)
        {
            var books = await _bookRepository.GetAll(pageSize, pageNumber, AuthorId, CategoryId, searchByTitle);
            var result = books.Select(book => new BookDetailsDto
            {
                BookID = book.BookID,
                Title = book.Title,
                PublishYear = book.PublishYear,
                Price = book.Price,
                AuthorID = book.AuthorID,
                CategoryID = book.CategoryID,
                Quantity = book.Quantity,
                AuthorName = book.Author?.FullName ?? "",
                CategoryName = book.Category?.Name ?? "",
                CoverImage = book.CoverImage ?? ""
            });
            return ApiResponse<IEnumerable<BookDetailsDto>>.Success(result);
        }

        public async Task<ApiResponse<BookDetailsDto>> GetById(int id)
        {
            var book = await _bookRepository.GetById(id);
            if (book == null)
                throw new NotFoundException("Book not found");

            var result = new BookDetailsDto
            {
                BookID = book.BookID,
                Title = book.Title,
                PublishYear = book.PublishYear,
                Price = book.Price,
                AuthorID = book.AuthorID,
                CategoryID = book.CategoryID,
                Quantity = book.Quantity,
                AuthorName = book.Author?.FullName ?? "",
                CategoryName = book.Category?.Name ?? "",
                CoverImage = book.CoverImage ?? ""
            };
            return ApiResponse<BookDetailsDto>.Success(result);
        }

        public async Task<ApiResponse> Add(CreateBookDto bookDto)
        {
            var author = await _authorRepository.GetById(bookDto.AuthorID);
            if (author == null)
                throw new NotFoundException("Author not found");

            var category = await _categoryRepository.GetById(bookDto.CategoryID);
            if (category == null)
                throw new NotFoundException("Category not found");

            var book = new Book
            {
                Title = bookDto.Title,
                PublishYear = bookDto.PublishYear,
                Price = bookDto.Price,
                AuthorID = bookDto.AuthorID,
                CategoryID = bookDto.CategoryID,
                Quantity = bookDto.Quantity,
                CoverImage = bookDto.CoverImage?.FileName ?? ""
            };
            await _bookRepository.Add(book);
            return ApiResponse.Success("Book created successfully");
        }

        public async Task<ApiResponse> Update(int id, CreateBookDto bookDto)
        {
            var book = await _bookRepository.GetById(id);
            if (book == null)
                throw new NotFoundException("Book not found");

            var author = await _authorRepository.GetById(bookDto.AuthorID);
            if (author == null)
                throw new NotFoundException("Author not found");

            var category = await _categoryRepository.GetById(bookDto.CategoryID);
            if (category == null)
                throw new NotFoundException("Category not found");

            book.Title = bookDto.Title;
            book.PublishYear = bookDto.PublishYear;
            book.Price = bookDto.Price;
            book.AuthorID = bookDto.AuthorID;
            book.CategoryID = bookDto.CategoryID;
            book.Quantity = bookDto.Quantity;
            if (bookDto.CoverImage != null)
                book.CoverImage = bookDto.CoverImage.FileName;

            await _bookRepository.Update(book);
            return ApiResponse.Success("Book updated successfully");
        }

        public async Task<ApiResponse> Delete(int id)
        {
            var book = await _bookRepository.GetById(id);
            if (book == null)
                throw new NotFoundException("Book not found");

            await _bookRepository.Delete(book);
            return ApiResponse.Success("Book deleted successfully");
        }
    }
}

