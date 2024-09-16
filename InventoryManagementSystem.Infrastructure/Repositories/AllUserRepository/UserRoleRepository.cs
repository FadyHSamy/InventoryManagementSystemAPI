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
    public class UserRoleRepository : IUserRolesRepository
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;
        public UserRoleRepository(IUnitOfWork unitOfWork)
        {
            _connection = unitOfWork.Connection;
            _transaction = unitOfWork.Transaction;
        }
        public async Task<UserRoles> GetUserRoleDescription(int userRoleId)
        {
            try
            {
                var storedProcedure = "[usr].[GetUserRoleDescription]";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("RoleId", userRoleId, dbType: DbType.Int32);
                parameters.Add("RoleName", "", dbType: DbType.String, direction: ParameterDirection.Output);

                await _connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: _transaction);
                string userRoleDescription = parameters.Get<string>("RoleName");
                UserRoles userRole = new UserRoles()
                {
                    RoleId = userRoleId,
                    RoleName = userRoleDescription
                };
                return userRole;

            }
            catch (Exception ex)
            {
                throw new DatabaseException("Error While Fetching user role description.", ex);
            }
        }
    }
}
