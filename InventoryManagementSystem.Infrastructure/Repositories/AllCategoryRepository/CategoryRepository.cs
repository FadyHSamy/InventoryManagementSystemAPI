using Dapper;
using InventoryManagementSystem.Core.Entities.Category;
using InventoryManagementSystem.Core.Entities.User;
using InventoryManagementSystem.Core.Exceptions;
using InventoryManagementSystem.Core.Interfaces.Repositories.AllCategoryIRepository;
using InventoryManagementSystem.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Repositories.AllCategoryRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DapperContext _dapperContext;
        public CategoryRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task DeleteCategory(int categoryId)
        {
            try
            {
                var storedProcedure = "[ims].[DeleteCategory]";
                using (var connection = _dapperContext.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("CategoryId", categoryId, dbType: DbType.Int64);

                    await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                    _dapperContext.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Error While Deleting Category.", ex);
            }
        }

        public async Task<List<Category>> GetAllCategories()
        {
            try
            {
                var storedProcedure = "[ims].[GetAllCategories]";
                using (var connection = _dapperContext.CreateConnection())
                {
                    var categories = await connection.QueryAsync<Category>(storedProcedure, commandType: CommandType.StoredProcedure);
                    _dapperContext.Dispose();
                    return categories.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Error While Fetching Categories.", ex);
            }
        }

        public async Task<Category> GetCategory(int categoryId)
        {
            try
            {
                var storedProcedure = "[ims].[GetSpecificCategory]";
                using (var connection = _dapperContext.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("CategoryId", categoryId, dbType: DbType.Int64);

                    Category categories = await connection.QueryFirstOrDefaultAsync<Category>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                    _dapperContext.Dispose();
                    return categories;
                }
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Error While Fetching Category.", ex);
            }
        }

        public async Task InsertCategory(string categoryName)
        {
            try
            {
                var storedProcedure = "[ims].[AddingCategory]";
                using (var connection = _dapperContext.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("CategoryName", categoryName, dbType: DbType.String);

                    await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                    _dapperContext.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Error While Inserting New Category.", ex);
            }
        }
    }
}
