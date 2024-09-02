using InventoryManagementSystem.Core.Entities;
using InventoryManagementSystem.Core.Entities.User;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        // Additional methods like Get, Update, Delete can be included here
    }
}