using LibraryManagementAPI.Models;

namespace LibraryManagementAPI.Services
{
    public interface IAuthorService
    {
        public Task<IEnumerable<Author>> GetAll();
        public Task<Author> GetById(int id);
        public Task<Author> Add(Author author);
        public Author Update(Author author);
        public Author Delete(Author author);
        public Task<bool> IsValid(int id);
    }
}
