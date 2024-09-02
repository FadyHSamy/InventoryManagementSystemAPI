using InventoryManagementSystem.Core.DTOs.User;
using InventoryManagementSystem.Core.Entities.Shared;
using System.Threading.Tasks;
namespace InventoryManagementSystem.Core.Interfaces.Services
{
    public interface IUserService
    {
        Task<ResultStatus> AddUserAsync(AddingUserDto addUserDto);
    }
}