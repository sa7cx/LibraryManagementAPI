using Application.Interfaces.IServices;
using AutoMapper;
using AutoMapper.Execution;
using LibraryManagementAPI.DTOs;
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
        private readonly IMapper _mapper;

        public MemberController(IMemberService memberService, IMapper mapper)
        {
            _memberService = memberService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMembers()
        {
            var members = await _memberService.GetAll();
            var result = _mapper.Map<IEnumerable<MemberDetailsDto>>(members);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMemberById(int id)
        {
            var member = await _memberService.GetById(id);
            if(member == null)
            {
                return NotFound($"Id {id} is not found");
            }
            var result = _mapper.Map<MemberDetailsDto>(member);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMember(CreateMemberDto dto)
        {
            var member = _mapper.Map<LibraryManagementAPI.Models.Member>(dto);
            await _memberService.Add(member);
            var result = _mapper.Map<MemberDetailsDto>(member);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember(CreateMemberDto dto,int id)
        {
            var member = await _memberService.GetById(id);
            if(member == null)
            {
                return NotFound($"Id {id} is not Found");            
            }
            _mapper.Map(dto,member);
            _memberService.Update(member);
            var result = _mapper.Map<MemberDetailsDto>(member);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            var member = await _memberService.GetById(id);
            if (member == null)
                return NotFound($"Id {id} is not found");
            _memberService.Delete(member);
            var result = _mapper.Map<MemberDetailsDto>(member);
            return Ok(result);
        }

    }
}
