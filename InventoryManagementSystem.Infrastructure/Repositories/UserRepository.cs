using Dapper;
using InventoryManagementSystem.Core.Entities.Shared;
using InventoryManagementSystem.Core.Entities.User;
using InventoryManagementSystem.Core.Interfaces.Repositories;
using InventoryManagementSystem.Infrastructure.Context;
using Microsoft.Data.SqlClient;
using System.Data;

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
            try
            {
                var storedProcedure = "[usr].[AddingUser]";
                using (var connection = _dapperContext.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("Username", user.Username, dbType: DbType.String);
                    parameters.Add("HashedPassword", user.PasswordHash, dbType: DbType.String);
                    parameters.Add("MobileNumber", user.MobileNumber, dbType: DbType.String);
                    parameters.Add("Email", user.Email, dbType: DbType.String);

                    //Execute stored procedure and map the returned result to a Customer object  
                    await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                    _dapperContext.DisposeConnection(connection);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}