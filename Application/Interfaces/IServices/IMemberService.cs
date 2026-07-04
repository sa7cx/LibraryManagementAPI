using Application.DTOs.Member;
using LibraryManagementAPI.Models;

namespace Application.Interfaces.IServices
{
    public interface IMemberService
    {
        public Task<ApiResponse<IEnumerable<MemberDetailsDto>>> GetAll();
        public Task<ApiResponse<MemberDetailsDto>> GetById(int id);
        public Task<ApiResponse> Add(CreateMemberDto memberDto);
        public Task<ApiResponse> Update(int id,CreateMemberDto memberDto);
        public Task<ApiResponse> Delete(int id);
    }
}
