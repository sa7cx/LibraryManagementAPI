namespace LibraryManagementAPI.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string FullName { get; set; }
        public string Country { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
