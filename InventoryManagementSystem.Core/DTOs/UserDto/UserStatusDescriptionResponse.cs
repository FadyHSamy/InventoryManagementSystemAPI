using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.DTOs.UserDto
{
    public class UserStatusDescriptionResponse
    {
        public int StatusId { get; set; }
        public required string StatusDescripton { get; set; }
    }
}
