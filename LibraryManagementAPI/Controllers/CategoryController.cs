using Application;
using Application.DTOs.Category;
using Application.Interfaces.IServices;
using AutoMapper;
using LibraryManagementAPI.Models;
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
            var res = await _categoryService.GetAll();
            return Ok(res);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var res = await _categoryService.GetById(id);
            return Ok(res);
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(CreateCategoryDto dto)
        {
            var res = await _categoryService.Add(dto);
            return Ok(res);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CreateCategoryDto dto)
        {
            var res = await _categoryService.Update(id, dto);
            return Ok(res);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var res = await _categoryService.Delete(id);
            return Ok(res);

        }

    }
}
