using InventoryManagementSystem.Infrastructure.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Context
{
    public interface IDapperContext
    {
        IDbConnection CreateConnection(); // To create a connection to the database
    }
    public class DapperContext : IDapperContext, IDisposable
    {
        private readonly string _connectionString;
        private IDbConnection _connection;

        public DapperContext(IOptions<DatabaseSettings> dbSettings)
        {
            _connectionString = dbSettings.Value.DefaultConnection;
        }

        public IDbConnection CreateConnection()
        {
            _connection = new SqlConnection(_connectionString); // Create SQL Server connection
            _connection.Open(); // Open the connection
            return _connection;
        }

        // Close and dispose of the connection properly
        public void Dispose()
        {
            if (_connection != null && _connection.State != ConnectionState.Closed)
            {
                _connection.Close();
                _connection.Dispose();
                _connection = null; // Clear connection object after disposing
            }
        }
    }
}
