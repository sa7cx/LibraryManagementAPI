using LibraryManagementAPI.DTOs;
using LibraryManagementAPI.Models;
using LibraryManagementAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly ICategoryService _categoryService;

        private List<string> _allowedExtentions = new List<string> { ".jpg", ".jpeg", ".png" };

        public BookController(IBookService bookService, IAuthorService authorService, ICategoryService categoryService)
        {
            _bookService = bookService;
            _authorService = authorService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookService.GetAll();
            return Ok(books);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _bookService.GetById(id);
            if (book == null)
            {
                return NotFound($"Id {id} is not found");
            }
            return Ok(book);
        }
        [HttpGet("byAuthorId/{id}")]
        public async Task<IActionResult> GetBooksByAuthorId(int id)
        {
            var books = await _bookService.GetAll(AuthorId:id);
            return Ok(books);
        }
        [HttpGet("byCategoryId/{id}")]
        public async Task<IActionResult> GetBooksByCategoryId(int id)
        {
            var books = await _bookService.GetAll(CategoryId: id);
            return Ok(books);
        }
        [HttpPost]
        public async Task<IActionResult> Createbook([FromForm] CreateBookDto dto)
        {
            if (!_allowedExtentions.Contains(Path.GetExtension(dto.CoverImage.FileName).ToLower()))
            {
                return BadRequest("only .jpg .png .jpeg are allowed");
            }

            var IsValidCategoryId = await _authorService.IsValid(dto.AuthorID);
            var IsValidAuthorId = await _categoryService.IsValid(dto.CategoryID);
            if (!IsValidAuthorId)
                return BadRequest("invalid AuthorId");
            if (!IsValidAuthorId)
                return BadRequest("invalid CategoryId");
            var IsBookTitleExist = await _bookService.IsBookTitleExist(dto.Title);
            if (IsBookTitleExist)
                return BadRequest("title is alredy exist");

            string filename = Guid.NewGuid + Path.GetExtension(dto.CoverImage.FileName);
            string foldePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","images","books");
            Directory.CreateDirectory(foldePath);
            var filePath = Path.Combine(foldePath,filename);
            using var stream = new FileStream(filePath, FileMode.Create);
            await dto.CoverImage.CopyToAsync(stream);

            var book = new Book
            {
                Title = dto.Title,
                PublishYear = dto.PublishYear,
                Price = dto.Price,
                CoverImage = filePath,
                Quantity = dto.Quantity,
                AuthorID = dto.AuthorID,
                CategoryID = dto.CategoryID,
            };
            _bookService.Add(book);
            return Ok(book);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook([FromForm] UpdateBookDto dto , int id)
        {
            var book = await _bookService.GetById(id);
            if (book == null)
            {
                return NotFound($"Id {id} is not found");
            }
            var IsValidCategoryId = await _authorService.IsValid(dto.AuthorID);
            var IsValidAuthorId = await _categoryService.IsValid(dto.CategoryID);
            if (!IsValidAuthorId)
                return BadRequest("invalid AuthorId");
            if (!IsValidAuthorId)
                return BadRequest("invalid CategoryId");
            var IsBookTitleExist = await _bookService.IsBookTitleExist(dto.Title,id);
            if (IsBookTitleExist)
                return BadRequest("title is alredy exist");

            if(dto.CoverImage != null)
            {
                if (!_allowedExtentions.Contains(Path.GetExtension(dto.CoverImage.FileName).ToLower()))
                {
                    return BadRequest("only .jpg .png .jpeg are allowed");
                }
                string filename = Guid.NewGuid + Path.GetExtension(dto.CoverImage.FileName);
                string foldePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "books");
                Directory.CreateDirectory(foldePath);
                var filePath = Path.Combine(foldePath, filename);
                using var stream = new FileStream(filePath, FileMode.Create);
                await dto.CoverImage.CopyToAsync(stream);
                book.CoverImage = filePath;
            }

            book.Title = dto.Title;
            book.AuthorID = dto.AuthorID;
            book.Quantity = dto.Quantity;
            book.CategoryID = dto.CategoryID;
            book.PublishYear = dto.PublishYear;
            book.Price = dto.Price;
            _bookService.Update(book);
            return Ok(book);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _bookService.GetById(id);
            if(book == null)
            {
                return NotFound($"Id {id} is not found");
            }
            _bookService.Delete(book);
            return Ok(book);
        }
           
    }
}
