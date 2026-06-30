using Application.Interfaces.IServices;
using LibraryManagementAPI.Models;

namespace LibraryManagementAPI.Services
{
    public class BookService : IBookService
    {
        private readonly AppDbContext _context;

        public BookService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Book>> GetAll(int? AuthorId = null, int? CategoryId = null,
            int? pageSize = null, int? pageNumber = null,string? searchByTitle = null)
        {
            var books = _context.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .AsQueryable();

            if (AuthorId.HasValue)
                books = books.Where(b => b.AuthorID == AuthorId);
            if (CategoryId.HasValue)
                books = books.Where(b => b.CategoryID == CategoryId);
            if(pageSize.HasValue && pageNumber.HasValue)
                books = books.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
            if (searchByTitle != null)
                books = books.Where(b => b.Title.Contains(searchByTitle));
            return await books.ToListAsync();
        }
        public async Task<Book> GetById(int id)
        {
           var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .SingleOrDefaultAsync(b => b.BookID == id);
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
            book.IsDeleted = true;
            _context.SaveChanges();
            return book ;
        }

        public async Task<bool> IsBookTitleExist(string title,int? id = null)
        {
            return await _context.Books.AnyAsync(b => b.Title == title && b.BookID != id);
        }

        public async Task<bool> Isvalid(int id)
        {
            return await _context.Books.AnyAsync(b => b.BookID == id);
        }

        
    }
}
