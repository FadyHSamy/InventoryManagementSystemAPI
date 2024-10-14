using InventoryManagementSystem.Core.DTOs.AuthDto;
using InventoryManagementSystem.Core.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.Interfaces.Repositories.AllAuthRepository
{
    public interface IAuthRepository
    {
        Task<User> ValidateUser(LoginRequestDto loginRequestDto);
    }
}
