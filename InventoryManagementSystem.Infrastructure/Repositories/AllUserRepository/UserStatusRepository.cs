using Dapper;
using InventoryManagementSystem.Core.Entities.User;
using InventoryManagementSystem.Core.Exceptions;
using InventoryManagementSystem.Core.Interfaces.Repositories.AllSharedIRepository;
using InventoryManagementSystem.Core.Interfaces.Repositories.AllUserIRepository;
using InventoryManagementSystem.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Repositories.AllUserRepository
{
    public class UserStatusRepository : IUserStatusRepository
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;
        public UserStatusRepository(IUnitOfWork unitOfWork)
        {
            _connection = unitOfWork.Connection;
            _transaction = unitOfWork.Transaction;
        }

        public async Task<UserStatus> GetUserStatusDescription(int userStatusId)
        {
            try
            {
                var storedProcedure = "[usr].[GetUserStatusDescription]";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("UserStatusId", userStatusId, dbType: DbType.Int32);
                parameters.Add("UserStatusDescription", "", dbType: DbType.String, direction: ParameterDirection.Output);

                await _connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: _transaction);
                string userStatusDescription = parameters.Get<string>("UserStatusDescription");
                UserStatus userStatus = new UserStatus()
                {
                    StatusId = userStatusId,
                    StatusDescripton = userStatusDescription
                };
                return userStatus;

            }
            catch (Exception ex)
            {
                throw new DatabaseException("Error While Fetching user status description.", ex);
            }
        }
    }
}
