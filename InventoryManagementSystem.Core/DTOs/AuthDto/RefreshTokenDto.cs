using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.DTOs.AuthDto
{
    public class RefreshTokenRequestDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
    public class TokenResponseDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
