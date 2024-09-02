using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.Entities.User
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public int StatusId { get; set; }
        public int RoleId { get; set; }
    }
}
