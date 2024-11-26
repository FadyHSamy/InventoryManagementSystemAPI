using InventoryManagementSystem.Core.DTOs.CategoryDto;
using InventoryManagementSystem.Core.DTOs.UserDto;
using InventoryManagementSystem.Core.Entities.Category;
using InventoryManagementSystem.Core.Entities.Shared;
using InventoryManagementSystem.Core.Exceptions;
using InventoryManagementSystem.Core.Interfaces.Services.AllCategoryIServices;
using InventoryManagementSystem.Core.Utilities.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("GetCategory")]
        public async Task<IActionResult> GetCategory([FromQuery] int CategoryId)
        {
            try
            {
                if (CategoryId == 0)
                {
                    throw new ValidationCustomException("CategoryId Required");
                }
                GetCategoryResponse categoryResponse = await _categoryService.GetCategory(CategoryId);
                ApiResponse<object> response = ApiResponseHelper.Success<object>(Request, null, new { category = categoryResponse });
                return Ok(response);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                List<GetCategoriesResponse> getCategoriesResponses = await _categoryService.GetAllCategories();
                ApiResponse<object> response = ApiResponseHelper.Success<object>(Request, null, new { categories = getCategoriesResponses });
                return Ok(response);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("InsertCategory")]
        public async Task<IActionResult> InsertCategory([FromQuery] string CategoryName)
        {
            try
            {
                if (CategoryName == null)
                {
                    throw new ValidationCustomException("CategoryName Required");
                }
                await _categoryService.InsertCategory(CategoryName);
                ApiResponse<object> response = ApiResponseHelper.Success<object>(Request, "Category Added Successfully");
                return Ok(response);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory([FromQuery] int CategoryId)
        {
            try
            {
                if (CategoryId == 0)
                {
                    throw new ValidationCustomException("CategoryId Required");
                }
                await _categoryService.DeleteCategory(CategoryId);
                ApiResponse<object> response = ApiResponseHelper.Success<object>(Request, "Category Deleted successfully");
                return Ok(response);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
