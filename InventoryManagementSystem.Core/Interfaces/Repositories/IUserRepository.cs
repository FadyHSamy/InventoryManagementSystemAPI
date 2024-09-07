using InventoryManagementSystem.Core.Entities;
using InventoryManagementSystem.Core.Entities.Shared;
using InventoryManagementSystem.Core.Entities.User;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
    }
}