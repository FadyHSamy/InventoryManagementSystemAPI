using Dapper;
using InventoryManagementSystem.Core.Entities.Shared;
using InventoryManagementSystem.Core.Entities.User;
using InventoryManagementSystem.Core.Exceptions;
using InventoryManagementSystem.Core.Interfaces.Repositories.AllLoggingIRepository;
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
        private readonly DapperContext _dapperContext;
        public LoggingRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public async Task InsertLoggingError(LoggingError loggingError)
        {
            try
            {
                var storedProcedure = "[ims].[InsertLoggingError]";
                using (var connection = _dapperContext.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("LogLevel", loggingError.LogLevel);
                    parameters.Add("Message", loggingError.Message);
                    parameters.Add("Exception", loggingError.Exception);
                    parameters.Add("InnerException", loggingError.InnerException);
                    parameters.Add("StackTrace", loggingError.StackTrace);
                    parameters.Add("ApplicationName", loggingError.ApplicationName);
                    parameters.Add("UserID", loggingError.UserID);
                    parameters.Add("MachineName", loggingError.MachineName);
                    parameters.Add("Source", loggingError.Source);
                    parameters.Add("RequestID", loggingError.RequestID);
                    parameters.Add("AdditionalInfo", loggingError.AdditionalInfo);

                    await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                    _dapperContext.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Error While Inserting Logging Error In Database.", ex);
            }
        }
    }
}
