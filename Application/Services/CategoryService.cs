using Application;
using Application.DTOs.Category;
using Application.Exceptions;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using LibraryManagementAPI.Models;

namespace LibraryManagementAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<ApiResponse<IEnumerable<CategoryDetailsDto>>> GetAll()
        {
            var categories = await _categoryRepository.GetAll();
            var res = categories.Select(c => new CategoryDetailsDto
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Description = c.Description
            });
            return ApiResponse<IEnumerable<CategoryDetailsDto>>.Success(res);
        }

        public async Task<ApiResponse<CategoryDetailsDto>> GetById(int id)
        {
            var category = await _categoryRepository.GetById(id);
            if (category == null)
                throw new NotFoundException("Category not found");
            var res = new CategoryDetailsDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description
            };
            return ApiResponse<CategoryDetailsDto>.Success(res);
        }

        public async Task<ApiResponse> Add(CreateCategoryDto categoryDto)
        {
            var category = new Category
            {
                Name = categoryDto.Name,
                Description = categoryDto.Description
            };
            await _categoryRepository.Add(category);
            return ApiResponse.Success("Category added successfully");
        }


        public async Task<ApiResponse> Update(int id,CreateCategoryDto categoryDto)
        {
            var category = await _categoryRepository.GetById(id);
            if (category == null)
                throw new NotFoundException("Category not found");
            category.Name = categoryDto.Name;
            category.Description = categoryDto.Description;
            await _categoryRepository.Update(category);
            return ApiResponse.Success("Category updated successfully");
        }

        public async Task<ApiResponse> Delete(int id)
        {
            var category = await _categoryRepository.GetById(id);
            if (category == null)
                throw new NotFoundException("Category not found");
            await _categoryRepository.Delete(category);
            return ApiResponse.Success("Category deleted successfully");
        }


    }
}
