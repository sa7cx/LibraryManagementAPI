using Application;
using Application.DTOs.Author;
using Application.Interfaces.IServices;
using AutoMapper;
using LibraryManagementAPI.Models;
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
            return Ok(ApiResponse<IEnumerable<AuthorDetailsDto>>.Success(authors));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            var author = await _authorService.GetById(id);
            return Ok(ApiResponse<AuthorDetailsDto>.Success(author));
        }
        [HttpPost]
        public async Task<IActionResult> AddAuthor(CreateAuthorDto dto)
        {
            await _authorService.Add(dto);
            return Ok(ApiResponse.Success("Added Successfuly"));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(CreateAuthorDto dto, int id)
        {
            await _authorService.Update(id, dto);
            return Ok(ApiResponse.Success("Updated Successfuly"));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            await _authorService.Delete(id);
            return Ok(ApiResponse.Success("Deleted Successfuly"));
        }

    }
}
