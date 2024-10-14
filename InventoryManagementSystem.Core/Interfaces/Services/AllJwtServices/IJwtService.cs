using InventoryManagementSystem.Core.DTOs.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.Interfaces.Services.AllJwtServices
{
    public interface IJwtService
    {
        string GenerateToken(UserInformationResponse userInformationResponse);
        ClaimsPrincipal ValidateToken(string token);
        Task<string> GenerateUserToken(string username);
    }
}
