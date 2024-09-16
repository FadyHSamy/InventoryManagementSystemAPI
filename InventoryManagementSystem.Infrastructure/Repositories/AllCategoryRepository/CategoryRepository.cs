using Dapper;
using InventoryManagementSystem.Core.Entities.Category;
using InventoryManagementSystem.Core.Entities.User;
using InventoryManagementSystem.Core.Exceptions;
using InventoryManagementSystem.Core.Interfaces.Repositories.AllCategoryIRepository;
using InventoryManagementSystem.Core.Interfaces.Repositories.AllSharedIRepository;
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
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;
        public CategoryRepository(IUnitOfWork unitOfWork)
        {
            _connection = unitOfWork.Connection;
            _transaction = unitOfWork.Transaction;
        }

        public async Task DeleteCategory(int categoryId)
        {
            try
            {
                var storedProcedure = "[ims].[DeleteCategory]";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("CategoryId", categoryId, dbType: DbType.Int64);

                await _connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: _transaction);

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

                var categories = await _connection.QueryAsync<Category>(storedProcedure, commandType: CommandType.StoredProcedure, transaction: _transaction);
                return categories.ToList();

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

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("CategoryId", categoryId, dbType: DbType.Int64);

                Category categories = await _connection.QueryFirstOrDefaultAsync<Category>(storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: _transaction);

                return categories;

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

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("CategoryName", categoryName, dbType: DbType.String);

                await _connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure, transaction: _transaction);

            }
            catch (Exception ex)
            {
                throw new DatabaseException("Error While Inserting New Category.", ex);
            }
        }
    }
}
