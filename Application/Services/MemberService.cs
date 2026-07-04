using Application;
using Application.DTOs.Member;
using Application.Exceptions;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using LibraryManagementAPI.Models;
using System.Diagnostics.CodeAnalysis;

namespace LibraryManagementAPI.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<ApiResponse<IEnumerable<MemberDetailsDto>>> GetAll()
        {
            var members = await _memberRepository.GetAll();
             var res = members.Select(m => new MemberDetailsDto
            {
                MemberID = m.MemberID,
                FullName = m.FullName,
                Email = m.Email,
                Phone = m.Phone
            });
            return ApiResponse<IEnumerable<MemberDetailsDto>>.Success(res) ;
        }

        public async Task<ApiResponse<MemberDetailsDto>> GetById(int id)
        {
            var member = await _memberRepository.GetById(id);
            if (member == null)
                throw new NotFoundException("Member not found");
            var res = new MemberDetailsDto
            {
                MemberID = member.MemberID,
                FullName = member.FullName,
                Email = member.Email,
                Phone = member.Phone
            };
            return ApiResponse<MemberDetailsDto>.Success(res);
        }

        public async Task<ApiResponse> Add(CreateMemberDto memberDto)
        {
           var member = new Member
           {
               FullName = memberDto.FullName,
               Email = memberDto.Email,
               Phone = memberDto.Phone
           };
           await _memberRepository.Add(member);
            return ApiResponse.Success("Member added successfully");
        }

        public async Task<ApiResponse> Update(int id,CreateMemberDto memberDto)
        {
            var member = await _memberRepository.GetById(id);
            if (member == null)
                throw new NotFoundException("Member not found");
            member.FullName = memberDto.FullName;
            member.Email = memberDto.Email;
            member.Phone = memberDto.Phone;
            await _memberRepository.Update(member);
            return ApiResponse.Success("Member updated successfully");
        }

        public async Task<ApiResponse> Delete(int id)
        {
            var member = await _memberRepository.GetById(id);
            if (member == null)
                throw new NotFoundException("Member not found");
            await _memberRepository.Delete(member);
            return ApiResponse.Success("Member deleted successfully");
        }

    }
}
