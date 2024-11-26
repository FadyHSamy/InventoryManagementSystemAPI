using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.DTOs.AuthDto
{
    public class LoginRequestDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class LoginResponseDto
    {
        public string token { get; set; }
        public UserLoginResponse user { get; set; }
    }
    public class UserLoginResponse
    {
        public string username { get; set; }
        public string role { get; set; }
    }
}
