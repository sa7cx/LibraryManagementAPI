using Application.DTOs.Auth;
using Application.Exceptions;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    
    public class AuthenticationSerice : IAuthenticationService
    {
        private readonly IAuthenticationRepository _authnticationRepository;
        private readonly GenerateToken _generateToken;
        public AuthenticationSerice(IAuthenticationRepository authnticationRepository,GenerateToken generateToken)
        {
            _authnticationRepository = authnticationRepository;
            _generateToken = generateToken;

        }
        public async Task<ApiResponse<ApplicationUser>> GetUserByEmailAsync(string email)
        {
            var user = await _authnticationRepository.GetUserByEmailAsync(email);
            if (user == null)
                throw new NotFoundException($"User with email '{email}' not found.");
            return ApiResponse<ApplicationUser>.Success(user);
        }

        public async Task<ApiResponse> LoginAsync(LoginDto loginDto)
        {
            var user = await _authnticationRepository.GetUserByEmailAsync(loginDto.Email);
            if (user == null)
                throw new NotFoundException($"User with email '{loginDto.Email}' not found.");
            var passwordValid = await _authnticationRepository.CheckPasswordAsync(user, loginDto.Password);
            if(!passwordValid)
                throw new UnauthorizedException("كلمة المرور المدخلة غير صحيحة");
            var token = await _generateToken.GenerateJwtToken(user.Id,loginDto.Email);
            return ApiResponse.Success(token);
        }

        public async Task<ApiResponse> RegisterAsync(RegisterDto registerDto)
        {
            var result = await _authnticationRepository.CreateAsync(registerDto);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                throw new BadRequestException(string.Join(", ", errors));
            }
            return ApiResponse.Success("تم انشاء الحساب بنجاح");
        }
    }
}
