using InventoryManagementSystem.Core.Interfaces.Repositories.AllSharedIRepository;
using InventoryManagementSystem.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Repositories.AllSharedRepository
{
    public class UnitOfWork:IUnitOfWork, IDisposable
    {
        private readonly IDbConnection _connection;
        private IDbTransaction _transaction;
        public UnitOfWork(IDapperContext dapperContext)
        {
            _connection = dapperContext.CreateConnection();
        }

        // Begin a new transaction
        public void BeginTransaction()
        {
            if (_transaction == null)
            {
                _transaction = _connection.BeginTransaction();
            }
        }

        // Commit the transaction
        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await Task.Run(() => _transaction.Commit());
                _transaction.Dispose();
                _transaction = null; // Clean up transaction object after commit
            }
        }

        // Rollback the transaction in case of failure
        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await Task.Run(() => _transaction.Rollback());
                _transaction.Dispose();
                _transaction = null; // Clean up transaction object after rollback
            }
        }

        // Dispose the transaction and connection
        public void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Dispose();
        }

        // Expose the connection and transaction to the repositories
        public IDbConnection Connection => _connection;
        public IDbTransaction Transaction => _transaction;
    }
}
