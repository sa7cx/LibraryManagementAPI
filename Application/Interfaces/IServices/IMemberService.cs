using LibraryManagementAPI.Models;

namespace Application.Interfaces.IServices
{
    public interface IMemberService
    {
        public Task<IEnumerable<Member>> GetAll();
        public Task<Member> GetById(int id);
        public Task<Member> Add(Member member);
        public Member Update(Member member);
        public Member Delete(Member member);
        public Task<bool> ISValid(int id);
    }
}
