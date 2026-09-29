using Application.DTOs.Auth;
using Application.Interfaces.IRepositories;
using Domain.Models;
using LibraryManagementAPI.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _context;
        public RefreshTokenRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(RefreshTokenDto refreshToken)
        {
            var refreshTokenEntity = new RefreshToken
            {
                Token = refreshToken.Token,
                ExpiresAt = refreshToken.ExpiresAt,
                CreatedAt = refreshToken.CreatedAt,
                UserId = refreshToken.UserId,
                User = refreshToken.User
            };
            await _context.RefreshTokens.AddAsync(refreshTokenEntity);
            _context.SaveChanges();
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            var refreshToken = await _context.RefreshTokens.Include(rt => rt.User).FirstOrDefaultAsync(rt => rt.Token == token);
            return refreshToken;
        }
      
       
    }
}
