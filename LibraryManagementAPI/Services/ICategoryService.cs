using LibraryManagementAPI.Models;

namespace LibraryManagementAPI.Services
{
    public interface ICategoryService
    {
        public  Task<IEnumerable<Category>> GetAll();
        public  Task<Category> GetById(int id);
        public Task<Category> Add(Category category);
        public Category Update(Category category);
        public Category Delete(Category category);
        public Task<bool> IsValid(int id);
    }
}
