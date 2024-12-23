using InventoryManagementSystem.Core.DTOs.InventoryDto;
using InventoryManagementSystem.Core.Entities.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.Interfaces.Services.AllInventoryIServices
{
    public interface IInventoryServices
    {
        Task<GetProductInventoryResponse> GetProductInventory(int productId);
        Task InsertProductInventory(InsertProductInventoryRequest insertProductInventoryRequest);
        Task DeleteProductInventory(int productId);
    }
}
