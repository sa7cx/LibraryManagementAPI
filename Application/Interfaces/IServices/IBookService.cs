using Application.DTOs.Book;
using LibraryManagementAPI.Models;

namespace Application.Interfaces.IServices
{
    public interface IBookService
    {
        public Task<ApiResponse<IEnumerable<BookDetailsDto>>> GetAll(int pageSize = 10, int pageNumber = 1, 
            int? AuthorId = null, int? CategoryId = null, string? searchByTitle = null);
        public Task<ApiResponse<BookDetailsDto>> GetById(int id);
        public Task<ApiResponse> Add(CreateBookDto bookDto);
        public Task<ApiResponse> Update(int id, CreateBookDto bookDto);
        public Task<ApiResponse> Delete(int id);
    }
}
