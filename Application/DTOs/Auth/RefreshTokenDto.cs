using Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth
{
    public class RefreshTokenDto
    {
       public string Token { get; set; }
       public DateTime ExpiresAt { get; set; }
       public DateTime CreatedAt { get; set; }
        public string UserId {get; set; }
        public ApplicationUser User { get; set; }
    }
}
