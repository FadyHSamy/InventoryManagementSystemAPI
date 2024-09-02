using Dapper;
using InventoryManagementSystem.Core.Entities;
using InventoryManagementSystem.Core.Entities.User;
using InventoryManagementSystem.Core.Interfaces;
using InventoryManagementSystem.Infrastructure.Context;
using System.Data;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _dapperContext;

        public UserRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task AddAsync(User user)
        {
            
        }
    }
}