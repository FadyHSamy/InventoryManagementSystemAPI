using InventoryManagementSystem.Core.Entities.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.Interfaces.Repositories.AllCategoryIRepository
{
    public interface ICategoryRepository
    {
        Task InsertCategory(string categoryName);
        Task<Category> GetCategory(int categoryId);
        Task<List<Category>> GetAllCategories();
        Task DeleteCategory(int categoryId);
    }
}
