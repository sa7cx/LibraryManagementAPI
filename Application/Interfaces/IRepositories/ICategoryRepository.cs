using LibraryManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories
{
    public interface ICategoryRepository
    {
        public Task<IEnumerable<Category>> GetAll();
        public Task<Category> GetById(int id);
        public Task Add(Category category);
        public Task Update(Category category);
        public Task Delete(Category category);
    }
}
