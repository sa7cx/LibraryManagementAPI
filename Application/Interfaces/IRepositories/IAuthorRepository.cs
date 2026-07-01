using LibraryManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories
{
    public interface IAuthorRepository
    {
        public Task<IEnumerable<Author>> GetAll();
        public Task<Author> GetById(int id);
        public Task Add(Author author);
        public Task Update(Author author);
        public Task Delete(Author author);
    }
}
