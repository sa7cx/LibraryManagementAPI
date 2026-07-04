using Application.DTOs.Borrow;
using LibraryManagementAPI.Models;

namespace Application.Interfaces.IServices
{
    public interface IBorrowService
    {
        public Task<ApiResponse<IEnumerable<BorrowingDetailsDto>>> GetAll();
        public Task<ApiResponse<BorrowingDetailsDto>> GetById(int id);
        public Task<ApiResponse> Borrow(CreateBorrowingDto borrowDto);
        public Task<ApiResponse> Return(int id);
    }
}
