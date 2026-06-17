using LibraryManagementAPI.Data;
using LibraryManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementAPI.Services
{
    public class BookService : IBookService
    {
        private readonly AppDbContext _context;

        public BookService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAll(int? AuthorId = null , int? CategoryId = null)
        {
            var books =  _context.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Select(b => new Book
            {
                BookID = b.BookID,
                Title = b.Title,
                PublishYear = b.PublishYear,
                Price = b.Price,
                AuthorID = b.AuthorID,
                CategoryID = b.CategoryID,
                Quantity = b.Quantity,
                CoverImage = b.CoverImage,
                Category = b.Category,
                Author = b.Author
            }).AsQueryable();

            if(AuthorId.HasValue)
                books = books.Where(b => b.AuthorID == AuthorId);
            if(CategoryId.HasValue)
                books = books.Where(b => b.CategoryID == CategoryId);

            return await books.ToListAsync();
        }

        public async Task<Book> GetById(int id)
        {
           var book = await _context.Books.SingleOrDefaultAsync(b => b.BookID == id);
            return book;
        }

        public async Task<Book> Add(Book book)
        {
            await _context.Books.AddAsync(book);
            _context.SaveChanges();
            return book;
        }

        public Book Update(Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();
            return book;
        }

        public Book Delete(Book book)
        {
            _context.Books.Remove(book);
            _context.SaveChanges();
            return book;
        }

        public async Task<bool> IsBookTitleExist(string title,int? id = null)
        {
            return await _context.Books.AnyAsync(b => b.Title == title && b.BookID != id);
        }

    }
}
