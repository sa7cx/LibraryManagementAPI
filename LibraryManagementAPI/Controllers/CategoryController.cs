using AutoMapper;
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
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
           var categories = await _categoryService.GetAll();
            var result = _mapper.Map<IEnumerable<CategoryDetailsDto>>(categories);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetById(id);
            if (category == null)
            {
                return NotFound($"the Id {id} is not found");
            }
            var result = _mapper.Map<CategoryDetailsDto>(category);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory (CreateCategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            await _categoryService.Add(category);
            var result = _mapper.Map<CategoryDetailsDto>(category);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CreateCategoryDto dto)
        {
            var category = await _categoryService.GetById(id);
            if (category == null)
            {
                return NotFound($"the Id {id} is not found");
            }
            _mapper.Map(dto,category);
            _categoryService.Update(category);
            var result = _mapper.Map<CategoryDetailsDto>(category);
            return Ok(result);
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
            var result = _mapper.Map<CategoryDetailsDto>(category);
            return Ok(result);
        }

    }
}
