using InventoryManagementSystem.Core.DTOs.CategoryDto;
using InventoryManagementSystem.Core.DTOs.InventoryDto;
using InventoryManagementSystem.Core.Entities.Shared;
using InventoryManagementSystem.Core.Exceptions;
using InventoryManagementSystem.Core.Interfaces.Services.AllInventoryIServices;
using InventoryManagementSystem.Core.Utilities.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryServices _inventoryServices;
        public InventoryController(IInventoryServices inventoryServices)
        {
            _inventoryServices = inventoryServices;
        }
        [HttpGet("GetProductInventory")]
        public async Task<IActionResult> GetProductInventory([FromQuery] int ProductId)
        {
            try
            {
                if (ProductId == 0)
                {
                    throw new ValidationCustomException("ProductId Required");
                }
                GetProductInventoryResponse inventoryResponse = await _inventoryServices.GetProductInventory(ProductId);
                ApiResponse<object> response = ApiResponseHelper.Success<object>(Request, null, new { inventory = inventoryResponse });
                return Ok(response);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost("InsertProductInventory")]
        public async Task<IActionResult> InsertProductInventory([FromBody] InsertProductInventoryRequest insertProductInventoryRequest)
        {
            try
            {
                if (insertProductInventoryRequest == null)
                {
                    throw new ValidationCustomException("insertProductInventoryRequest Required");
                }
                await _inventoryServices.InsertProductInventory(insertProductInventoryRequest);
                ApiResponse<object> response = ApiResponseHelper.Success<object>(Request, "Inventory Added Successfully");
                return Ok(response);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
