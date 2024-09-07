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
        IDbConnection CreateConnection();
    }
    public class DapperContext : IDapperContext, IDisposable
    {
        private readonly string _connectionString;
        private IDbConnection _connection;

        public DapperContext(IOptions<DatabaseSettings> dbSettings)
        {
            _connectionString = dbSettings?.Value?.DefaultConnection ?? throw new ArgumentNullException(nameof(dbSettings));
        }

        public IDbConnection CreateConnection()
        {
            _connection = new SqlConnection(_connectionString);
            _connection.Open();
            return _connection;
        }

        // Properly manage the connection lifecycle
        public void Dispose()
        {
            DisposeConnection();
            GC.SuppressFinalize(this);
        }

        // Method to close and dispose of the connection
        private void DisposeConnection()
        {
            if (_connection != null && _connection.State != ConnectionState.Closed)
            {
                _connection.Close();
                _connection.Dispose();
                _connection = null; // Set to null after disposal
            }
        }
    }
}
