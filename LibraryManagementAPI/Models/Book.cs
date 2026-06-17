using System.ComponentModel.DataAnnotations;

namespace LibraryManagementAPI.Models
{
    public class Book
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public DateTime PublishYear { get; set; }

        public decimal Price { get; set; }
        public int AuthorID { get; set; }
        public int CategoryID { get; set; }

        public int Quantity { get; set; }
        public string CoverImage { get; set; }
        public bool IsDeleted { get; set; } = false ;

        public Author Author { get; set; }
        public Category Category { get; set; }
        public ICollection<BorrowRecord> BorrowRecords { get; set; }

    }
}
