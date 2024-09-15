using Dapper;
using InventoryManagementSystem.Core.Entities.Category;
using InventoryManagementSystem.Core.Entities.Product;
using InventoryManagementSystem.Core.Exceptions;
using InventoryManagementSystem.Core.Interfaces.Repositories.AllProductIRepository;
using InventoryManagementSystem.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Repositories.AllProductRepository
{
    public class ProductRepository: IProductRepository
    {
        private readonly DapperContext _dapperContext;
        public ProductRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<List<Product>> GetProducts()
        {
            try
            {
                var storedProcedure = "[ims].[GetProducts]";
                using (var connection = _dapperContext.CreateConnection())
                {
                    var products = await connection.QueryAsync<Product>(storedProcedure, commandType: CommandType.StoredProcedure);
                    _dapperContext.Dispose();
                    return products.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Error While Fetching Products.", ex);
            }
        }

        public async Task InsertProduct(Product Product)
        {
            try
            {
                var storedProcedure = "[ims].[AddingProduct]";
                using (var connection = _dapperContext.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("ProductName", Product.ProductName, dbType: DbType.String);
                    parameters.Add("ProductDescription", Product.ProductDescription, dbType: DbType.String);
                    parameters.Add("ProductPrice", Product.ProductPrice, dbType: DbType.Decimal);
                    parameters.Add("CategoryId", Product.CategoryId, dbType: DbType.Int64);

                    await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                    _dapperContext.Dispose();
                }
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
                using (var connection = _dapperContext.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("ProductId", ProductId, dbType: DbType.Int64);

                    await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                    _dapperContext.Dispose();
                }
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
                using (var connection = _dapperContext.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("ProductId", ProductId, dbType: DbType.Int64);

                    Product product = await connection.QueryFirstOrDefaultAsync<Product>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                    _dapperContext.Dispose();
                    return product;
                }
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Error While Fetching Product.", ex);
            }
        }
    }
}
