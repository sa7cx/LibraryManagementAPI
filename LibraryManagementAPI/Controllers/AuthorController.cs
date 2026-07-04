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
            return Ok(authors);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            var res = await _authorService.GetById(id);
            return Ok(res);
        }
        [HttpPost]
        public async Task<IActionResult> AddAuthor(CreateAuthorDto dto)
        {
            var res = await _authorService.Add(dto);
            return Ok(res);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(CreateAuthorDto dto, int id)
        {
            var res = await _authorService.Update(id, dto);
            return Ok(res);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var res = await _authorService.Delete(id);
            return Ok(res);
        }

    }
}
