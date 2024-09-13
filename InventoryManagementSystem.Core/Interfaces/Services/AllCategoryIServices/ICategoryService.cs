using InventoryManagementSystem.Core.DTOs.CategoryDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.Interfaces.Services.AllCategoryIServices
{
    public interface ICategoryService
    {
        Task InsertCategory(string categoryName);
        Task<GetCategoryResponse> GetCategory(int categoryId);
        Task<List<GetCategoriesResponse>> GetAllCategories();
        Task DeleteCategory(int categoryId);
    }
}
