using Application.Common;
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
        public Task LogoutAsync(string refreshToken);
        public Task<ApiResponse<object>> RefreshTokenAsync(string token);
        public  string GenerateRefreshToken();
        public string GenerateJwtToken(string userId, string email);
        public Task<ApiResponse> RegisterAsync(RegisterDto registerDto);
        public Task<ApiResponse<ApplicationUser>> GetUserByEmailAsync(string email);
        public Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginDto loginDto);
    }
}
