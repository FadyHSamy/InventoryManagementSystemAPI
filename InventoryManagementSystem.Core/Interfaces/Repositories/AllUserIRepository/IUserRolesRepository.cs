using InventoryManagementSystem.Core.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.Interfaces.Repositories.AllUserIRepository
{
    public interface IUserRolesRepository
    {
        Task<UserRoles> GetUserRoleDescription(int userRoleId);
    }
}
