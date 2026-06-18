using LibraryManagementAPI.Data;
using LibraryManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementAPI.Services
{
    public class MemberService : IMemberService
    {
        private readonly AppDbContext _context;

        public MemberService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Member>> GetAll()
        {
            var members = await _context.Members
                .Select(m => new Member
                {
                    MemberID = m.MemberID,
                    FullName = m.FullName,
                    Email = m.Email,
                    Phone = m.Phone,                   
                })
            .ToListAsync();
            return members;
        }

        public async Task<Member> GetById(int id)
        {
            var member = await _context.Members.FirstOrDefaultAsync(m => m.MemberID == id);
            return member;
        }

        public async Task<Member> Add(Member member)
        {
            await _context.Members.AddAsync(member);
            _context.SaveChanges();
            return member;
        }

        public Member Update(Member member)
        {
            _context.Members.Update(member);
            _context.SaveChanges();
            return member;
        }
        public Member Delete(Member member)
        {
            _context.Members.Remove(member);
            _context.SaveChanges(); 
            return member;
        }


    }
}
