using Application.DTOs.Category;
using LibraryManagementAPI.Models;

namespace Application.Interfaces.IServices
{
    public interface ICategoryService
    {
        public  Task<IEnumerable<CategoryDetailsDto>> GetAll();
        public  Task<CategoryDetailsDto> GetById(int id);
        public Task Add(CreateCategoryDto categoryDto);
        public Task Update(int id,CreateCategoryDto categoryDto);
        public Task Delete(int id);
    }
}
