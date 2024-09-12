using InventoryManagementSystem.Core.Entities;
using InventoryManagementSystem.Core.Entities.Shared;
using InventoryManagementSystem.Core.Entities.User;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.Interfaces.Repositories.AllUserIRepository
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        Task<User> GetUserInformation(string Username);
    }
}