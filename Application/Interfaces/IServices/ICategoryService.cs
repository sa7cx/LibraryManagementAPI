using Application.DTOs.Category;
using LibraryManagementAPI.Models;

namespace Application.Interfaces.IServices
{
    public interface ICategoryService
    {
        public  Task<ApiResponse<IEnumerable<CategoryDetailsDto>>> GetAll();
        public  Task<ApiResponse<CategoryDetailsDto>> GetById(int id);
        public Task<ApiResponse> Add(CreateCategoryDto categoryDto);
        public Task<ApiResponse> Update(int id,CreateCategoryDto categoryDto);
        public Task<ApiResponse> Delete(int id);
    }
}
