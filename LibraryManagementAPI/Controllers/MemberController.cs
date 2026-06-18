using LibraryManagementAPI.DTOs;
using LibraryManagementAPI.Models;
using LibraryManagementAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMembers()
        {
            var members = await _memberService.GetAll();
            return Ok(members);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMemberById(int id)
        {
            var member = await _memberService.GetById(id);
            if(member == null)
            {
                return NotFound($"Id {id} is not found");
            }
            return Ok(member);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMember(CreateMemberDto dto)
        {
            var member = new Member
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Phone = dto.Phone,
            };
            await _memberService.Add(member);
            return Ok(member);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember(CreateMemberDto dto,int id)
        {
            var member = await _memberService.GetById(id);
            if(member == null)
            {
                return NotFound($"Id {id} is not Found");            
            }
            member.FullName = dto.FullName;
            member.Email = dto.Email;
            member.Phone = dto.Phone;
            _memberService.Update(member);
            return Ok(member);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            var member = await _memberService.GetById(id);
            if (member == null)
                return NotFound($"Id {id} is not found");
            _memberService.Delete(member);
            return Ok(member);
        }

    }
}
