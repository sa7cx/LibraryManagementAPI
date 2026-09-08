using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Auth;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces.IRepositories
{
    public interface IAuthenticationRepository
    {
        public Task<IdentityResult> CreateAsync(RegisterDto registerDto);
        public Task<ApplicationUser?> GetUserByEmailAsync(string email);
        public Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
    }
}
