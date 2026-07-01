using LibraryManagementAPI.Models;

namespace Application.DTOs.Borrow
{
    public class BorrowingDetailsDto
    {
        public int Id  { get; set; }
        public int BookID { get; set; }
        public int MemberID { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public BorrowStatus status { get; set; }
    }
}
