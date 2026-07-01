//using Application.DTOs.Book;
//using Application.Interfaces.IServices;
//using AutoMapper;
//using LibraryManagementAPI.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace LibraryManagementAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class BookController : ControllerBase
//    {
//        private readonly IBookService _bookService;
//        private readonly IAuthorService _authorService;
//        private readonly ICategoryService _categoryService;
//        private readonly IMapper _mapper;
//        private List<string> _allowedExtentions = new List<string> { ".jpg", ".jpeg", ".png" };

//        public BookController(IBookService bookService, IAuthorService authorService, ICategoryService categoryService,IMapper mapper)
//        {
//            _bookService = bookService;
//            _authorService = authorService;
//            _categoryService = categoryService;
//            _mapper = mapper;
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetAllBooks( int? pageSize ,int? PageNumber, string? SearchByTitle )
//        {
//            var books = await _bookService.GetAll(pageSize: pageSize,pageNumber: PageNumber,searchByTitle: SearchByTitle);
//            var result = _mapper.Map<IEnumerable<BookDetailsDto>>(books);
//            return Ok(result);
//        }
//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetBookById(int id)
//        {
//            var book = await _bookService.GetById(id);
//            if (book == null)
//            {
//                return NotFound($"Id {id} is not found");
//            }
//            var result = _mapper.Map<BookDetailsDto>(book);
//            return Ok(result);
//        }
//        [HttpGet("byAuthorId/{id}")]
//        public async Task<IActionResult> GetBooksByAuthorId(int id)
//        {
//            var isvalid = await _authorService.IsValid(id);
//            if (!isvalid)
//                return BadRequest("invalid AuthorId");
            
//            var books = await _bookService.GetAll(AuthorId:id);
//            var result = _mapper.Map<IEnumerable<BookDetailsDto>>(books);
//            return Ok(result);
//        }
//        [HttpGet("byCategoryId/{id}")]
//        public async Task<IActionResult> GetBooksByCategoryId(int id)
//        {
//            var isvali = await _categoryService.IsValid(id);
//            if(!isvali)
//                return BadRequest("invalid CategoryID");
//            var books = await _bookService.GetAll(CategoryId: id);
//            var result = _mapper.Map<IEnumerable<BookDetailsDto>>(books);
//            return Ok(result);
//        }
//        [HttpPost]
//        public async Task<IActionResult> Createbook([FromForm] CreateBookDto dto)
//        {
//            if (!_allowedExtentions.Contains(Path.GetExtension(dto.CoverImage.FileName).ToLower()))
//            {
//                return BadRequest("only .jpg .png .jpeg are allowed");
//            }

//            var IsValidCategoryId = await _authorService.IsValid(dto.CategoryID);
//            var IsValidAuthorId = await _categoryService.IsValid(dto.AuthorID);
//            if (!IsValidAuthorId)
//                return BadRequest("invalid AuthorId");
//            if (!IsValidCategoryId)
//                return BadRequest("invalid CategoryId");
//            var IsBookTitleExist = await _bookService.IsBookTitleExist(dto.Title);
//            if (IsBookTitleExist)
//                return BadRequest("title is alredy exist");

//            string filename = Path.GetExtension(dto.CoverImage.FileName);
//            string foldePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","images","books");
//            Directory.CreateDirectory(foldePath);
//            var filePath = Path.Combine(foldePath,filename);
//            using var stream = new FileStream(filePath, FileMode.Create);
//            await dto.CoverImage.CopyToAsync(stream);

//            var book = _mapper.Map<Book>(dto);

//            book.CoverImage = filePath;

//            await _bookService.Add(book);
//            var result = _mapper.Map<BookDetailsDto>(book);
//            return Ok(result);
//        }
//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdateBook([FromForm] UpdateBookDto dto , int id)
//        {
//            var book = await _bookService.GetById(id);
//            if (book == null)
//            {
//                return NotFound($"Id {id} is not found");
//            }
//            var IsValidCategoryId = await _authorService.IsValid(dto.AuthorID);
//            var IsValidAuthorId = await _categoryService.IsValid(dto.CategoryID);
//            if (!IsValidAuthorId)
//                return BadRequest("invalid AuthorId");
//            if (!IsValidAuthorId)
//                return BadRequest("invalid CategoryId");
//            var IsBookTitleExist = await _bookService.IsBookTitleExist(dto.Title,id);
//            if (IsBookTitleExist)
//                return BadRequest("title is alredy exist");

//            if(dto.CoverImage != null)
//            {
//                if (!_allowedExtentions.Contains(Path.GetExtension(dto.CoverImage.FileName).ToLower()))
//                {
//                    return BadRequest("only .jpg .png .jpeg are allowed");
//                }
//                string filename = Path.GetExtension(dto.CoverImage.FileName);
//                string foldePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "books");
//                Directory.CreateDirectory(foldePath);
//                var filePath = Path.Combine(foldePath, filename);
//                using var stream = new FileStream(filePath, FileMode.Create);
//                await dto.CoverImage.CopyToAsync(stream);
//                book.CoverImage = filePath;
//            }

//             _mapper.Map(dto,book);
//            _bookService.Update(book);
//            var result = _mapper.Map<BookDetailsDto>(book);
//            return Ok(result);
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteBook(int id)
//        {
//            var book = await _bookService.GetById(id);
//            if(book == null)
//            {
//                return NotFound($"Id {id} is not found");
//            }
//            _bookService.Delete(book);
//            var result = _mapper.Map<BookDetailsDto>(book);
//            return Ok(result);
//        }
           
//    }
//}
