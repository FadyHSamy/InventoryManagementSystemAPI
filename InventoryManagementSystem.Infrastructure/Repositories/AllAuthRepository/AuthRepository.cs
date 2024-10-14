using InventoryManagementSystem.Core.DTOs.AuthDto;
using InventoryManagementSystem.Core.Entities.User;
using InventoryManagementSystem.Core.Interfaces.Repositories.AllAuthRepository;
using InventoryManagementSystem.Core.Interfaces.Repositories.AllSharedIRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Repositories.AllAuthRepository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;
        public AuthRepository(IUnitOfWork unitOfWork)
        {
            _connection = unitOfWork.Connection;
            _transaction = unitOfWork.Transaction;
        }

        public async Task<User> ValidateUser(LoginRequestDto loginRequestDto)
        {
            throw new NotImplementedException();
        }
    }
}
