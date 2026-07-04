using LibraryManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories
{
    public interface IBookRepository
    {
        public Task<IEnumerable<Book>> GetAll(
            int pageSize,
            int pageNumber,
            int? AuthorId = null,
            int? CategoryId = null,
            string? searchByTitle = null
            );
        public Task<Book> GetById(int id);
        public Task Add(Book book);
        public Task Update(Book book);
        public Task Delete(Book book);
    }
}
