//using Application.DTOs.Borrow;
//using Application.Interfaces.IServices;
//using AutoMapper;
//using LibraryManagementAPI.Helper;
//using LibraryManagementAPI.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace LibraryManagementAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class BorrowingController : ControllerBase
//    {
//        private readonly IBookService _bookService;
//        private readonly IMemberService _memberService;
//        private readonly IBorrowService _borrowService;
//        private readonly IMapper _mapper;

//        public BorrowingController(IBookService bookService, IMemberService memberService, IBorrowService borrowService, IMapper mapper)
//        {
//            _bookService = bookService;
//            _memberService = memberService;
//            _borrowService = borrowService;
//            _mapper = mapper;
//        }

//        [HttpPost]
//        public async Task<IActionResult> CreateBoroow(CreateBorrowingDto dto)
//        {
//            var isvalidBook = await _bookService.Isvalid(dto.BookID);
//            var isvalidMember = await _memberService.ISValid(dto.MemberID);
//            if (!isvalidBook)
//                return BadRequest("book is valid");
//            if (!isvalidMember)
//                return BadRequest("Member is valid");
//            var book = await _bookService.GetById(dto.BookID);
//            if(book.Quantity == 0)
//                return BadRequest("not book avileble");
//            if (dto.ReturnDate <= dto.BorrowDate)
//                return BadRequest("ReturnDate can not be less than BorrowDate");

//            var borrow = _mapper.Map<BorrowRecord>(dto);            
//            await _borrowService.Borrowing(borrow,book);
//            var result = _mapper.Map<BorrowingDetailsDto>(borrow);
//            return Ok(result);

//        }

//        [HttpPut("{id}")]
//        public async Task<IActionResult> Returned(int id)
//        {
//            var borrow = await _borrowService.GetById(id);
//            if (borrow == null)
//                return NotFound($"id {id} is not found");
//            if (borrow.status == BorrowStatus.Avilable)
//                return BadRequest("the status is alredy avilable !");
//            var book = await _bookService.GetById(borrow.BookID);
//            if(book == null)
//                return NotFound($"the bookId {borrow.BookID} is not found");

//            _borrowService.Returned(borrow,book);
//            var result = _mapper.Map<BorrowingDetailsDto>(borrow);
//            return Ok(result);
//        }

//    }
//}
