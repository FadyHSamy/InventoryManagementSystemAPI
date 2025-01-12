using Dapper;
using InventoryManagementSystem.Core.Entities.Shared;
using InventoryManagementSystem.Core.Entities.User;
using InventoryManagementSystem.Core.Exceptions;
using InventoryManagementSystem.Core.Interfaces.Repositories.AllLoggingIRepository;
using InventoryManagementSystem.Core.Interfaces.Repositories.AllSharedIRepository;
using InventoryManagementSystem.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Repositories.AllLoggingRepository
{
    public class LoggingRepository : ILoggingRepository
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;
        public LoggingRepository(IUnitOfWork unitOfWork)
        {
            _connection = unitOfWork.Connection;
            _transaction = unitOfWork.Transaction;
        }
        public async Task InsertLoggingError(LoggingError loggingError)
        {
            try
            {
                var storedProcedure = "[ims].[InsertLoggingError]";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Message", loggingError.Message);
                parameters.Add("Exception", loggingError.Exception);
                parameters.Add("ApplicationName", loggingError.ApplicationName);
                parameters.Add("UserID", loggingError.UserID);
                parameters.Add("Source", loggingError.Source);
                parameters.Add("AdditionalInfo", loggingError.AdditionalInfo);

                await _connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: _transaction);

            }
            catch (Exception ex)
            {
                throw new DatabaseException("Error While Inserting Logging Error In Database.", ex);
            }
        }
    }
}
