namespace Application.DTOs.Borrow
{
    public class CreateBorrowingDto
    {
        public int BookID { get; set; }
        public int MemberID { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime BorrowDate { get; set; }
    }
}
