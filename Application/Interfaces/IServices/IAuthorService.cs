using Application.DTOs.Author;
using LibraryManagementAPI.Models;

namespace Application.Interfaces.IServices
{
    public interface IAuthorService
    {
        public Task<IEnumerable<AuthorDetailsDto>> GetAll();
        public Task<AuthorDetailsDto> GetById(int id);
        public Task Add(CreateAuthorDto authorDto);
        public Task Update(int id , CreateAuthorDto authorDto);
        public Task Delete(int id);

    }
}
