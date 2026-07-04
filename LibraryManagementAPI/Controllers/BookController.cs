using Application.DTOs.Book;
using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private List<string> _allowedExtentions = new List<string> { ".jpg", ".jpeg", ".png" };

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks(int pageSize = 10, int pageNumber = 1, int? AuthorId = null, 
            int? CategoryId = null, string? SearchByTitle = null)
        {
            var result = await _bookService.GetAll(pageSize, pageNumber, AuthorId, CategoryId, SearchByTitle);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var result = await _bookService.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromForm] CreateBookDto dto)
        {
            if (dto.CoverImage != null && !_allowedExtentions.Contains(Path.GetExtension(dto.CoverImage.FileName).ToLower()))
            {
                return BadRequest("Only .jpg, .png, .jpeg are allowed");
            }

            var result = await _bookService.Add(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook([FromForm] CreateBookDto dto, int id)
        {
            if (dto.CoverImage != null && !_allowedExtentions.Contains(Path.GetExtension(dto.CoverImage.FileName).ToLower()))
            {
                return BadRequest("Only .jpg, .png, .jpeg are allowed");
            }

            var result = await _bookService.Update(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _bookService.Delete(id);
            return Ok(result);
        }
    }
}

