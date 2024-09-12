using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.DTOs.UserDto
{
    public class UserInformationResponse
    {
        public string Username { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LoginSuccessfully { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string StatusDescripton { get; set; }
        public string RoleName { get; set; }
    }
}
