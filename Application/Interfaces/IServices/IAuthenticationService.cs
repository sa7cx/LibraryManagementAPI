using Application.DTOs.Auth;
using Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface IAuthenticationService
    {
        public Task<ApiResponse> RegisterAsync(RegisterDto registerDto);
        public Task<ApiResponse<ApplicationUser>> GetUserByEmailAsync(string email);
        public Task<ApiResponse> LoginAsync(LoginDto loginDto);
    }
}
