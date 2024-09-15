using InventoryManagementSystem.Core.DTOs.CategoryDto;
using InventoryManagementSystem.Core.DTOs.ProductDto;
using InventoryManagementSystem.Core.Entities.Shared;
using InventoryManagementSystem.Core.Exceptions;
using InventoryManagementSystem.Core.Interfaces.Services.AllProductIServices;
using InventoryManagementSystem.Core.Utilities.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }
        [HttpGet("GetProduct")]
        public async Task<IActionResult> GetProduct([FromQuery] int ProductId)
        {
            try
            {
                if (ProductId == 0)
                {
                    throw new ValidationCustomException("ProductId Required");
                }
                GetProductResponse productResponse = await _productService.GetProduct(ProductId);
                ApiResponse<object> response = ApiResponseHelper.Success<object>(Request, null, new { product = productResponse });
                return Ok(response);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                List<GetProductsResponse> getProductsResponse = await _productService.GetProducts();
                ApiResponse<object> response = ApiResponseHelper.Success<object>(Request, null, new { product = getProductsResponse });
                return Ok(response);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("InsertProduct")]
        public async Task<IActionResult> InsertProduct([FromBody] InsertProductRequest insertProductRequest)
        {
            _logger.LogInformation("Inserting product");
            try
            {
                await _productService.InsertProduct(insertProductRequest);
                ApiResponse<object> response = ApiResponseHelper.Success<object>(Request, "Product Added Successfully");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Inserting product");
                throw ex;
            }
        }

        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct([FromQuery] int ProductId)
        {
            try
            {
                if (ProductId == 0)
                {
                    throw new ValidationCustomException("ProductId Required");
                }
                await _productService.DeleteProduct(ProductId);
                ApiResponse<object> response = ApiResponseHelper.Success<object>(Request, "Product Deleted successfully");
                return Ok(response);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
