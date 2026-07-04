using Application.Interfaces.IRepositories;
using LibraryManagementAPI.Data;
using LibraryManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAll(
            int pageSize,
            int pageNumber,
            int? AuthorId = null,
            int? CategoryId = null,
            string? searchByTitle = null)
        {
            var query =  _context.Books.AsQueryable();

            if (AuthorId.HasValue)
            {
                query = query.Where(b => b.AuthorID == AuthorId.Value);
            }
            if (CategoryId.HasValue)
            {
                query = query.Where(b => b.CategoryID == CategoryId.Value);
            }
            if (!string.IsNullOrWhiteSpace(searchByTitle))
            {
                query = query.Where(b => b.Title.Contains(searchByTitle));
            }
            var data = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return data;

        }

        public async Task<Book> GetById(int id)
        {
            var book = await _context.Books.SingleOrDefaultAsync(b => b.BookID == id);
            return book;
        }

        public async Task Add(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Book book)
        {
             _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(Book book)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

    }
}
