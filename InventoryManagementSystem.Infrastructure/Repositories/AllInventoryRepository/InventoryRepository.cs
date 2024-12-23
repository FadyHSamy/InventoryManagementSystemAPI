using Dapper;
using InventoryManagementSystem.Core.Entities.Inventory;
using InventoryManagementSystem.Core.Entities.User;
using InventoryManagementSystem.Core.Exceptions;
using InventoryManagementSystem.Core.Interfaces.Repositories.AllInventoryIRepository;
using InventoryManagementSystem.Core.Interfaces.Repositories.AllSharedIRepository;
using InventoryManagementSystem.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Repositories.AllInventoryRepository
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;
        public InventoryRepository(IUnitOfWork unitOfWork)
        {
            _connection = unitOfWork.Connection;
            _transaction = unitOfWork.Transaction;
        }

        public async Task<Inventory> GetProductInventory(int productId)
        {
            try
            {
                var storedProcedure = "[ims].[GetInventory]";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("ProductId", productId, dbType: DbType.Int32);

                return await _connection.QueryFirstOrDefaultAsync<Inventory>(storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: _transaction);
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Error While Fetching Product Inventory.", ex);
            }
        }

        public async Task InsertProductInventory(Inventory inventory)
        {
            try
            {
                var storedProcedure = "[ims].[InsertProductInventory]";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("ProductId", inventory.ProductId, dbType: DbType.Int32);
                parameters.Add("StockQuantity", inventory.StockQuantity, dbType: DbType.Int32);

                await _connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: _transaction);
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Error While Inserting Product Inventory.", ex);
            }
        }
        
        public async Task DeleteProductInventory(int productId)
        {
            try
            {
                var storedProcedure = "[ims].[DeleteProductInventory]";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("ProductId", productId, dbType: DbType.Int32);

                await _connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: _transaction);
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Error While Deleting Product Inventory.", ex);
            }
        }
    
        public async Task AdjustProductInventory(int productId, int stockAdjustment)
        {
            try
            {
                var storedProcedure = "[ims].[AdjustProductInventory]";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("ProductId", productId, dbType: DbType.Int32);
                parameters.Add("StockAdjustment", stockAdjustment, dbType: DbType.Int32);

                await _connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: _transaction);
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Error While Adjusting Product Inventory.", ex);
            }
        }
    
    
    }
}
