using Application.Common;
using Application.DTOs.Auth;
using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Domain.Models;
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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    
    public class AuthenticationSerice : IAuthenticationService
    {
        private readonly IAuthenticationRepository _authnticationRepository;
        private readonly IConfiguration _configureOptions;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AuthenticationSerice
            (IAuthenticationRepository authnticationRepository,
            IConfiguration configureOptions , IRefreshTokenRepository refreshTokenRepository,IUnitOfWork unitOfWork)
        {
            _authnticationRepository = authnticationRepository;
            _configureOptions = configureOptions;
            _refreshTokenRepository = refreshTokenRepository;
            _unitOfWork = unitOfWork;

        }
        public async Task LogoutAsync(string refreshToken)
        {
           var RefreshToken = await _refreshTokenRepository.GetByTokenAsync(refreshToken);
            if (RefreshToken is null)
                throw new BadRequestException("Refresh token is invalid.");
            if (RefreshToken.ExpiresAt <= DateTime.UtcNow)
                throw new BadRequestException("Refresh token has expired.");
            if (RefreshToken.RevokedAt is not null)
                return;
            RefreshToken.RevokedAt = DateTime.UtcNow;
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ApiResponse<object>> RefreshTokenAsync(string refreshToken)
        {
            var RefreshToken = await _refreshTokenRepository.GetByTokenAsync(refreshToken);
            if(RefreshToken is null)
                throw new BadRequestException("Refresh token is invalid.");
            if(RefreshToken.ExpiresAt <= DateTime.UtcNow)
                throw new BadRequestException("Refresh token has expired.");
            if (RefreshToken.RevokedAt is not null)
                throw new BadRequestException("Refresh token has been revoked.");
            var user = RefreshToken.User;
            var newJwtToken = GenerateJwtToken(user.Id, user.Email!);
            var newRefreshToken = GenerateRefreshToken();
            RefreshToken.RevokedAt = DateTime.UtcNow;
            var newRefreshTokenEntity = new RefreshTokenDto
            {
             ExpiresAt = DateTime.UtcNow.AddDays(7),
             CreatedAt = DateTime.UtcNow,
             Token = newRefreshToken,
             UserId = user.Id,
             User = user
            };
            await _refreshTokenRepository.AddAsync(newRefreshTokenEntity);
            return ApiResponse<object>.Success(new { AccessToken = newJwtToken, RefreshToken = newRefreshToken });
        }
        public  string GenerateRefreshToken()
        {
            var randomBytes = RandomNumberGenerator.GetBytes(64);
            return Convert.ToBase64String(randomBytes);
        }

        public string GenerateJwtToken(string userId, string email)
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

        public async Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginDto loginDto)
        {
            var user = await _authnticationRepository.GetUserByEmailAsync(loginDto.Email);
            if (user == null)
                throw new NotFoundException($"User with email '{loginDto.Email}' not found.");
            var passwordValid = await _authnticationRepository.CheckPasswordAsync(user, loginDto.Password);
            if(!passwordValid)
                throw new UnauthorizedException("كلمة المرور المدخلة غير صحيحة");
            var token =  GenerateJwtToken(user.Id,loginDto.Email);
            var refreshToken = GenerateRefreshToken();
            await _refreshTokenRepository.AddAsync(new RefreshTokenDto
            {
                Token = refreshToken,
                UserId = user.Id,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                User = user
            });
            return ApiResponse<AuthResponseDto>.Success(new AuthResponseDto { AccessToken =token , RefreshToken = refreshToken});
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
