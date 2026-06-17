using LibraryManagementAPI.DTOs;
using LibraryManagementAPI.Models;
using LibraryManagementAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = await _authorService.GetAll();
            return Ok(authors);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            var author = await _authorService.GetById(id);
            if (author == null)
            {
                return NotFound($"the Id {id} is not found");
            }
            return Ok(author);
        }
        [HttpPost]
        public async Task<IActionResult> AddAuthor(CreateAuthorDto dto)
        {
            var author = new Author
            {
                FullName = dto.FullName,
                Country = dto.Country
            };
            await _authorService.Add(author);
            return Ok(author);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(CreateAuthorDto dto, int id)
        {
            var author = await _authorService.GetById(id);
            if (author == null)
                return NotFound($"the Id {id} is not found");
            author.FullName = dto.FullName;
            author.Country = dto.Country;
            _authorService.Update(author);
            return Ok(author);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _authorService.GetById(id);
            if(author == null)
                return NotFound($"the Id {id} is not found");
             _authorService.Delete(author);
            return Ok(author);
        }

    }
}
