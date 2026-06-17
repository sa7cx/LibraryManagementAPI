using LibraryManagementAPI.Models;

namespace LibraryManagementAPI.Services
{
    public interface IBookService
    {
        public Task<IEnumerable<Book>> GetAll();
        public Task<Book> GetById(int id);
        public Task<Book> Add(Book book);
        public Book Update(Book book);
        public Book Delete(Book book);
        public Task<bool> IsBookTitleExist(string title,int? id = null);

    }
}
