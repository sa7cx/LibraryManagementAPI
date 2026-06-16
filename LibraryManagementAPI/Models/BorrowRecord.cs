namespace LibraryManagementAPI.Models
{
    public enum BorrowStatus
    {
        Borrowed,
        Avilable
    }
    public class BorrowRecord
    {
        public int Id { get; set; }
        public int BookID { get; set; }
        public int MemberID { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public BorrowStatus status{ get; set; }

        public Book Book { get; set; }
        public Member Member { get; set; }
    }
}
