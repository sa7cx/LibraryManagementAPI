using Application.Interfaces.IServices;
using LibraryManagementAPI.Models;


namespace LibraryManagementAPI.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly AppDbContext _context;

        public AuthorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAll()
        {
            var authors = await _context.Authors.Select(a => new Author
            {
                AuthorId = a.AuthorId,
                FullName = a.FullName,
                Country = a.Country,
                Books = a.Books
            }).ToListAsync();
            return authors;
        }

        public async Task<Author> GetById(int id)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.AuthorId == id);
            return author;
        }
        public async Task<Author> Add(Author author)
        {
            await _context.Authors.AddAsync(author);
            _context.SaveChanges();
            return author;
        }
        public Author Update(Author author)
        {
            _context.Authors.Update(author);
            _context.SaveChanges();
            return author;
        }

        public Author Delete(Author author)
        {
            _context.Remove(author);
            _context.SaveChanges();
            return author;
        }

        public async Task<bool> IsValid(int id)
        {
            return await _context.Authors.AnyAsync(a => a.AuthorId == id);
        }


    }
}
