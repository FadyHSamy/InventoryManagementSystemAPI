using InventoryManagementSystem.Core.DTOs.User;
using InventoryManagementSystem.Core.Entities.Shared;

namespace InventoryManagementSystem.Core.Interfaces.Services
{
    public interface IUserService
    {
        Task AddUserAsync(AddingUserDto addUserDto);
    }
}