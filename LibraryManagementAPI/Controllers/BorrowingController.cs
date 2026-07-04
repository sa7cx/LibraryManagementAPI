using Application.DTOs.Borrow;
using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowingController : ControllerBase
    {
        private readonly IBorrowService _borrowService;

        public BorrowingController(IBorrowService borrowService)
        {
            _borrowService = borrowService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBorrows()
        {
            var result = await _borrowService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBorrowById(int id)
        {
            var result = await _borrowService.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> BorrowBook(CreateBorrowingDto dto)
        {
            if (dto.ReturnDate <= dto.BorrowDate)
                return BadRequest("ReturnDate cannot be less than or equal to BorrowDate");

            var result = await _borrowService.Borrow(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ReturnBook(int id)
        {
            var result = await _borrowService.Return(id);
            return Ok(result);
        }
    }
}

