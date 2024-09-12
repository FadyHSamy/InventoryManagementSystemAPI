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
    public class UserRoleRepository : IUserRolesRepository
    {
        private readonly DapperContext _dapperContext;
        public UserRoleRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task<UserRoles> GetUserRoleDescription(int userRoleId)
        {
            try
            {
                var storedProcedure = "[usr].[GetUserRoleDescription]";
                using (var connection = _dapperContext.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("RoleId", userRoleId, dbType: DbType.Int32);
                    parameters.Add("RoleName","", dbType: DbType.String, direction: ParameterDirection.Output);

                    await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                    _dapperContext.Dispose();
                    string userRoleDescription = parameters.Get<string>("RoleName");
                    UserRoles userRole = new UserRoles()
                    {
                        RoleId = userRoleId,
                        RoleName = userRoleDescription
                    };
                    return userRole;
                }
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Error While Fetching user role description.", ex);
            }
        }
    }
}
