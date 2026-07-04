using Application.DTOs.Author;
using LibraryManagementAPI.Models;

namespace Application.Interfaces.IServices
{
    public interface IAuthorService
    {
        public Task<ApiResponse<IEnumerable<AuthorDetailsDto>>> GetAll();
        public Task<ApiResponse<AuthorDetailsDto>> GetById(int id);
        public Task<ApiResponse> Add(CreateAuthorDto authorDto);
        public Task<ApiResponse> Update(int id , CreateAuthorDto authorDto);
        public Task<ApiResponse> Delete(int id);

    }
}
