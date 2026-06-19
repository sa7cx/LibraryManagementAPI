using System.ComponentModel.DataAnnotations;

namespace LibraryManagementAPI.DTOs
{
    public class BookDetailsDto
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public DateTime PublishYear { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        public int AuthorID { get; set; }
        public int CategoryID { get; set; }
        public int Quantity { get; set; }
        public string AuthorName { get; set; }
        public string CategoryName { get; set; }
        public string CoverImage { get; set; }
    }
}
