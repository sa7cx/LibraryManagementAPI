using Application.DTOs.Member;
using LibraryManagementAPI.Models;

namespace Application.Interfaces.IServices
{
    public interface IMemberService
    {
        public Task<IEnumerable<MemberDetailsDto>> GetAll();
        public Task<MemberDetailsDto> GetById(int id);
        public Task Add(CreateMemberDto memberDto);
        public Task Update(int id,CreateMemberDto memberDto);
        public Task Delete(int id);
    }
}
