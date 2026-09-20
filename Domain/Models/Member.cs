using Infrastructure.Identity;

namespace LibraryManagementAPI.Models
{
    public class Member
    {
        public int MemberID { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public DateTime JoinDate { get; set; } = DateTime.Now;

        public string userId { get; set; } 
        public ApplicationUser User { get; set; } 

        public ICollection<BorrowRecord> BorrowRecords { get; set; }
    }
}
