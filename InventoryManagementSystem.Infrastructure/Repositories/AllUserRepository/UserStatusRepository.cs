using Dapper;
using InventoryManagementSystem.Core.Entities.User;
using InventoryManagementSystem.Core.Exceptions;
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
        private readonly DapperContext _dapperContext;
        public UserStatusRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<UserStatus> GetUserStatusDescription(int userStatusId)
        {
            try
            {
                var storedProcedure = "[usr].[GetUserStatusDescription]";
                using (var connection = _dapperContext.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("UserStatusId", userStatusId, dbType: DbType.Int32);
                    parameters.Add("UserStatusDescription","", dbType: DbType.String, direction: ParameterDirection.Output);

                    await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                    _dapperContext.Dispose();
                    string userStatusDescription = parameters.Get<string>("UserStatusDescription");
                    UserStatus userStatus = new UserStatus()
                    {
                        StatusId = userStatusId,
                        StatusDescripton = userStatusDescription
                    };
                    return userStatus;
                }
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Error While Fetching user status description.", ex);
            }
        }
    }
}
