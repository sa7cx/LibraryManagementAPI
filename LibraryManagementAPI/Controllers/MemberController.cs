using Application;
using Application.DTOs.Member;
using Application.Interfaces.IServices;
using AutoMapper;
using AutoMapper.Execution;
using LibraryManagementAPI.Models;
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
           var res = await _memberService.GetAll();
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMemberById(int id)
        {
            var res = await _memberService.GetById(id);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMember(CreateMemberDto dto)
        {
            var res = await _memberService.Add(dto);
            return Ok(res);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember(CreateMemberDto dto, int id)
        {
            var res = await _memberService.Update(id, dto);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            var res = await _memberService.Delete(id);
            return Ok(res);
        }

    }
}
