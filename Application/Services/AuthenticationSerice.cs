using Application.Common;
using Application.DTOs.Auth;
using Application.Exceptions;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    
    public class AuthenticationSerice : IAuthenticationService
    {
        private readonly IAuthenticationRepository _authnticationRepository;
        private readonly IConfiguration _configureOptions;

        public AuthenticationSerice(IAuthenticationRepository authnticationRepository,IConfiguration configureOptions)
        {
            _authnticationRepository = authnticationRepository;
            _configureOptions = configureOptions;

        }

        public async Task<string> GenerateJwtToken(string userId, string email)
        {
            var clims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Email, email)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configureOptions["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configureOptions["Jwt:Issuer"],
                audience: _configureOptions["Jwt:Audience"],
                claims: clims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
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
            var token = await GenerateJwtToken(user.Id,loginDto.Email);
            return ApiResponse.Success(token);
        }

        public async Task<ApiResponse> RegisterAsync(RegisterDto registerDto)
        {
            var existingUser = await _authnticationRepository.GetUserByEmailAsync(registerDto.email);
            if (existingUser != null)
            {
                throw new ConflictException($"User with email '{registerDto.email}' already exists.");
            }

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
