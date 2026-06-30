using LibraryManagementAPI.Models;

namespace Application.Interfaces.IServices
{
    public interface IBookService
    {
        public Task<IEnumerable<Book>> GetAll(int? AuthorId = null , int? CategoryId = null
            ,int? pageSize = null, int? pageNumber = null,string? searchByTitle = null);
        public Task<Book> GetById(int id);
        public Task<Book> Add(Book book);
        public Book Update(Book book);
        public Book Delete(Book book);
        public Task<bool> IsBookTitleExist(string title,int? id = null);
        public Task<bool> Isvalid(int id);

    }
}
