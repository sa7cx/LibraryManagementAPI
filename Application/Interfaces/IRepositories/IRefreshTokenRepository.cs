using Application.DTOs.Auth;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories
{
    public interface IRefreshTokenRepository
    {
        public Task AddAsync(RefreshTokenDto refreshTokenDto);
        public Task<RefreshToken> GetByTokenAsync(string token);

    }
}
