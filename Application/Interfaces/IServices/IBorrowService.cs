using LibraryManagementAPI.Models;
using LibraryManagementAPI.DTOs;

namespace Application.Interfaces.IServices
{
    public interface IBorrowService
    {
        public Task<BorrowRecord> GetById(int id);
        public Task<BorrowRecord> Borrowing(BorrowRecord borrow, Book book);
        public BorrowRecord Returned(BorrowRecord borrow,Book book);
    }
}
