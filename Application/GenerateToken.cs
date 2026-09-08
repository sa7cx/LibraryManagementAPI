using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Application
{
    public class GenerateToken
    {
        private readonly IConfiguration _configureOptions;
        public GenerateToken(IConfiguration configureOptions)
        {
            _configureOptions = configureOptions;
        }
        public async Task<string>GenerateJwtToken(string userId, string email)
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


    }
}
