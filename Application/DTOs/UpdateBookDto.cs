using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementAPI.DTOs
{
    public class UpdateBookDto
    {
        [Required]
        public string Title { get; set; }
        public DateTime PublishYear { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        public int AuthorID { get; set; }
        public int CategoryID { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
        public IFormFile? CoverImage { get; set; }
    }
}
