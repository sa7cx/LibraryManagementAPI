using LibraryManagementAPI.DTOs;
using LibraryManagementAPI.Models;
using LibraryManagementAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
           var categories = await _categoryService.GetAll();
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetById(id);
            if (category == null)
            {
                return NotFound($"the Id {id} is not found");
            }
            return Ok(category);
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory (CreateCategoryDto dto)
        {
            var category = new Category
            {
                Name = dto.Name,
                Description = dto.Description
            };
            await _categoryService.Add(category);
            return Ok(category);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CreateCategoryDto dto)
        {
            var category = await _categoryService.GetById(id);
            if (category == null)
            {
                return NotFound($"the Id {id} is not found");
            }
            category.Name = dto.Name;
            category.Description = dto.Description;
            _categoryService.Update(category);
            return Ok(category);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryService.GetById(id);
            if (category == null)
            {
                return NotFound($"the Id {id} is not found");
            }
            _categoryService.Delete(category);
            return Ok(category);
        }

    }
}
