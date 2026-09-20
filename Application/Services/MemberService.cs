using Application.Common;
using Application.DTOs.Member;
using Application.Exceptions;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using LibraryManagementAPI.Models;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;

namespace LibraryManagementAPI.Services
{
    public class MemberService : IMemberService
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository, IAuthenticationRepository authenticationRepository)
        {
            _memberRepository = memberRepository;
            _authenticationRepository = authenticationRepository;

        }

        public async Task<ApiResponse<IEnumerable<MemberDetailsDto>>> GetAll()
        {
            var members = await _memberRepository.GetAll();
             var res = members.Select(m => new MemberDetailsDto
            {
                MemberID = m.MemberID,
                FullName = m.FullName,
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
                Phone = member.Phone
            };
            return ApiResponse<MemberDetailsDto>.Success(res);
        }

        public async Task<ApiResponse> Add(CreateMemberDto memberDto)
        {
            var userExists = await _authenticationRepository.GetUserById(memberDto.userId);
            if (userExists is null)
                throw new NotFoundException("User not found");
            var member = new Member
           {
               FullName = memberDto.FullName,
               Phone = memberDto.Phone,
               userId = memberDto.userId
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
