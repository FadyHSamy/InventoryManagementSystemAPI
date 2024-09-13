using InventoryManagementSystem.Core.DTOs.CategoryDto;
using InventoryManagementSystem.Core.Entities.Category;
using InventoryManagementSystem.Core.Exceptions;
using InventoryManagementSystem.Core.Interfaces.Repositories.AllCategoryIRepository;
using InventoryManagementSystem.Core.Interfaces.Services.AllCategoryIServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.Services.AllCategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task DeleteCategory(int categoryId)
        {
            try
            {
                Category category = await _categoryRepository.GetCategory(categoryId);
                if (category == null)
                { 
                    throw new NotFoundException("No Category Exist With This Id");
                }
                await _categoryRepository.DeleteCategory(categoryId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<GetCategoriesResponse>> GetAllCategories()
        {
            try
            {
                List<Category> categories = await _categoryRepository.GetAllCategories();
                if (categories.Count == 0)
                {
                    throw new NotFoundException("There Is No Categories");
                }
                List<GetCategoriesResponse> GetCategoriesResponse = categories.Select(cat => new GetCategoriesResponse
                {
                    CategoryId = cat.CategoryId,
                    CategoryName = cat.CategoryName,
                }).ToList();
                return GetCategoriesResponse;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<GetCategoryResponse> GetCategory(int categoryId)
        {
            try
            {
                Category category = await _categoryRepository.GetCategory(categoryId);
                if (category == null)
                {
                    throw new NotFoundException("Category Not Found");
                }
                GetCategoryResponse getCategoryResponse = new GetCategoryResponse()
                {
                    CategoryId = categoryId,
                    CategoryName = category.CategoryName
                };
                return getCategoryResponse;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task InsertCategory(string categoryName)
        {
            try
            {
                await _categoryRepository.InsertCategory(categoryName);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
