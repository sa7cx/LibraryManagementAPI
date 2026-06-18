namespace LibraryManagementAPI.DTOs
{
    public class CreateBorrowingDto
    {
        public int BookID { get; set; }
        public int MemberID { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
