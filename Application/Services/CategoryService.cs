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

        public async Task<IEnumerable<CategoryDetailsDto>> GetAll()
        {
            var categories = await _categoryRepository.GetAll();
            var res = categories.Select(c => new CategoryDetailsDto
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Description = c.Description
            });
            return res;
        }

        public async Task<CategoryDetailsDto> GetById(int id)
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
            return res;
        }

        public async Task Add(CreateCategoryDto categoryDto)
        {
            var category = new Category
            {
                Name = categoryDto.Name,
                Description = categoryDto.Description
            };
            await _categoryRepository.Add(category);
        }


        public async Task Update(int id,CreateCategoryDto categoryDto)
        {
            var category = await _categoryRepository.GetById(id);
            if (category == null)
                throw new NotFoundException("Category not found");
            category.Name = categoryDto.Name;
            category.Description = categoryDto.Description;
            await _categoryRepository.Update(category);
        }

        public async Task Delete(int id)
        {
            var category = await _categoryRepository.GetById(id);
            if (category == null)
                throw new NotFoundException("Category not found");
            await _categoryRepository.Delete(category);
        }


    }
}
