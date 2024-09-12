using InventoryManagementSystem.Core.DTOs.UserDto;
using InventoryManagementSystem.Core.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.Interfaces.Services.AllUserIServices
{
    public interface IUserRoleService
    {
        Task<UserRoleDescriptionResponse> GetUserRoleDescription(int userRoleId);
    }
}
