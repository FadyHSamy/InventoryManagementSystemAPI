using Dapper;
using InventoryManagementSystem.Core.Entities.Category;
using InventoryManagementSystem.Core.Entities.Product;
using InventoryManagementSystem.Core.Exceptions;
using InventoryManagementSystem.Core.Interfaces.Repositories.AllProductIRepository;
using InventoryManagementSystem.Core.Interfaces.Repositories.AllSharedIRepository;
using InventoryManagementSystem.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Repositories.AllProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;
        public ProductRepository(IUnitOfWork unitOfWork)
        {
            _connection = unitOfWork.Connection;
            _transaction = unitOfWork.Transaction;
        }

        public async Task<List<Product>> GetProducts()
        {
            try
            {
                var storedProcedure = "[ims].[GetProducts]";

                var products = await _connection.QueryAsync<Product>(storedProcedure, commandType: CommandType.StoredProcedure, transaction: _transaction);
                return products.ToList();

            }
            catch (Exception ex)
            {
                throw new DatabaseException("Error While Fetching Products.", ex);
            }
        }

        public async Task<int> InsertProduct(Product Product)
        {
            try
            {
                var storedProcedure = "[ims].[AddingProduct]";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("ProductName", Product.ProductName, dbType: DbType.String);
                parameters.Add("ProductDescription", Product.ProductDescription, dbType: DbType.String);
                parameters.Add("ProductPrice", Product.ProductPrice, dbType: DbType.Decimal);
                parameters.Add("CategoryId", Product.CategoryId, dbType: DbType.Int64);
                parameters.Add("ProductId", "", dbType: DbType.Decimal, direction: ParameterDirection.Output);

                await _connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: _transaction);

                int ProductId = parameters.Get<int>("ProductId");
                return ProductId;

            }
            catch (Exception ex)
            {
                throw new DatabaseException("Error While Inserting New Product.", ex);
            }
        }

        public async Task DeleteProduct(int ProductId)
        {
            try
            {
                var storedProcedure = "[ims].[DeleteProduct]";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("ProductId", ProductId, dbType: DbType.Int64);

                await _connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: _transaction);


            }
            catch (Exception ex)
            {
                throw new DatabaseException("Error While Deleting Product.", ex);
            }
        }

        public async Task<Product> GetProduct(int ProductId)
        {
            try
            {
                var storedProcedure = "[ims].[GetProduct]";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("ProductId", ProductId, dbType: DbType.Int64);

                Product product = await _connection.QueryFirstOrDefaultAsync<Product>(storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: _transaction);
                return product;

            }
            catch (Exception ex)
            {
                throw new DatabaseException("Error While Fetching Product.", ex);
            }
        }
    }
}
