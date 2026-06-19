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
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public AuthorController(IAuthorService authorService, IMapper mapper)
        {
            _authorService = authorService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = await _authorService.GetAll();
            var result = _mapper.Map<IEnumerable<AuthorDetailsDto>>(authors);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            var author = await _authorService.GetById(id);
            if (author == null)
            {
                return NotFound($"the Id {id} is not found");
            }
            var result = _mapper.Map<AuthorDetailsDto>(author);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddAuthor(CreateAuthorDto dto)
        {
            var author = _mapper.Map<Author>(dto);
            await _authorService.Add(author);
            var result = _mapper.Map<AuthorDetailsDto>(author);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(CreateAuthorDto dto, int id)
        {
            var author = await _authorService.GetById(id);
            if (author == null)
                return NotFound($"the Id {id} is not found");
            _mapper.Map(dto,author);
            _authorService.Update(author);
            var result = _mapper.Map<AuthorDetailsDto>(author);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _authorService.GetById(id);
            if(author == null)
                return NotFound($"the Id {id} is not found");
            _authorService.Delete(author);
            var result = _mapper.Map<AuthorDetailsDto>(author);
            return Ok(result);
        }

    }
}
