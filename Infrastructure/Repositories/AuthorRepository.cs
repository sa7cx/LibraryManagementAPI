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
    public class AuthorRepository: IAuthorRepository
    {
        private readonly AppDbContext _context;

        public AuthorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAll()
        {
            var authors = await _context.Authors.ToListAsync();
            return authors;
        }

        public async Task<Author> GetById(int id)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.AuthorId == id);
            return author;
        }

        public async Task Add(Author author)
        {
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Author author)
        {
             _context.Authors.Update(author);
             await _context.SaveChangesAsync();
        }

        public async Task Delete(Author author)
        {
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
        }

    }
}
