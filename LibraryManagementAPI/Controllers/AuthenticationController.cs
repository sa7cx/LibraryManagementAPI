using Application;
using Application.DTOs.Auth;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var result = await _authenticationService.RegisterAsync(registerDto);
            return Ok(result);
        }
        [HttpGet("GetUserByEmail")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var result = await _authenticationService.GetUserByEmailAsync(email);
            return Ok(result);
        }
        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn(LoginDto loginDto)
        {
           var result = await _authenticationService.LoginAsync(loginDto);
           return Ok(result);
        }
    }
}
