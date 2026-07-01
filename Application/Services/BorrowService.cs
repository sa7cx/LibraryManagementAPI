//using Application.Interfaces.IServices;
//using LibraryManagementAPI.DTOs;
//using LibraryManagementAPI.Models;

//namespace LibraryManagementAPI.Services
//{
//    public class BorrowService : IBorrowService
//    {
//        private readonly AppDbContext _context;
//        private readonly IBookService _bookService;

//        public BorrowService(AppDbContext context, IBookService bookService)
//        {
//            _context = context;
//            _bookService = bookService;
//        }

//        public async Task<BorrowRecord> Borrowing(BorrowRecord borrow , Book book)
//        {
//            book.Quantity --;
//            borrow.status = BorrowStatus.Borrowed;
//            await _context.AddAsync(borrow);
//            _context.SaveChanges();
//            return borrow;          
            
//        }

//        public async Task<BorrowRecord> GetById(int id)
//        {
//           var borrow = await _context.BorrowRecords.FirstOrDefaultAsync(br => br.Id == id);
//            return borrow;
//        }

//        public BorrowRecord Returned(BorrowRecord borrow,Book book)
//        {
//            book.Quantity++;
//            borrow.status = BorrowStatus.Avilable;
//            _context.Update(borrow);
//            _context.SaveChanges();
//            return borrow;
//        }
//    }
//}
