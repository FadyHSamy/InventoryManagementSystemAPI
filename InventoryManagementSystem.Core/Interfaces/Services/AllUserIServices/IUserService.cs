using InventoryManagementSystem.Core.DTOs.UserDto;
using InventoryManagementSystem.Core.Entities.Shared;

namespace InventoryManagementSystem.Core.Interfaces.Services.AllUserIServices
{
    public interface IUserService
    {
        Task AddUser(AddingUserRequest addUserDto);
        Task<UserInformationResponse> GetUserInformation(string Username);
        Task<string> GetUserHashPassword(string Username);
    }
}