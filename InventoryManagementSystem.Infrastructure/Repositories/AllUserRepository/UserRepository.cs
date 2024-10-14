using Dapper;
using InventoryManagementSystem.Core.Entities.Shared;
using InventoryManagementSystem.Core.Entities.User;
using InventoryManagementSystem.Core.Exceptions;
using InventoryManagementSystem.Core.Interfaces.Repositories.AllSharedIRepository;
using InventoryManagementSystem.Core.Interfaces.Repositories.AllUserIRepository;
using InventoryManagementSystem.Infrastructure.Context;
using Microsoft.Data.SqlClient;
using System.Data;

namespace InventoryManagementSystem.Infrastructure.Repositories.AllUserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;

        public UserRepository(IUnitOfWork unitOfWork)
        {
            _connection = unitOfWork.Connection;
            _transaction = unitOfWork.Transaction;
        }

        public async Task AddUser(User user)
        {
            try
            {
                var storedProcedure = "[usr].[AddingUser]";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Username", user.Username, dbType: DbType.String);
                parameters.Add("HashedPassword", user.PasswordHash, dbType: DbType.String);
                parameters.Add("MobileNumber", user.MobileNumber, dbType: DbType.String);
                parameters.Add("Email", user.Email, dbType: DbType.String);

                await _connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: _transaction); // Execute with transaction


            }
            catch (Exception ex)
            {
                throw new DatabaseException(ex.Message);
            }
        }

        public async Task<string> GetUserHashPassword(string Username)
        {
            try
            {
                var storedProcedure = "[usr].[GetUserHashPassword]";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Username", Username, dbType: DbType.String);
                parameters.Add("passwordHash", "", dbType: DbType.String, direction: ParameterDirection.Output);

                await _connection.QueryFirstOrDefaultAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: _transaction);
                string PasswordHashed = parameters.Get<string>("passwordHash");
                return PasswordHashed;

            }
            catch (Exception ex)
            {
                throw new DatabaseException("Error While Fetching user information.", ex);
            }
        }

        public async Task<User> GetUserInformation(string Username)
        {
            try
            {
                var storedProcedure = "[usr].[GetUserInformationByUsername]";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Username", Username, dbType: DbType.String);

                User UserInformation = await _connection.QueryFirstOrDefaultAsync<User>(storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: _transaction);
                return UserInformation;

            }
            catch (Exception ex)
            {
                throw new DatabaseException("Error While Fetching user information.", ex);
            }
        }
    }
}