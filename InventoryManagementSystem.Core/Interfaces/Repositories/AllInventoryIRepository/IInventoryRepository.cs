using InventoryManagementSystem.Core.Entities.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.Interfaces.Repositories.AllInventoryIRepository
{
    public interface IInventoryRepository
    {
        Task<Inventory> GetProductInventory(int productId);
        Task InsertProductInventory(Inventory inventory);
        Task DeleteProductInventory(int productId);
        Task AdjustProductInventory(int productId,int stockAdjustment);
    }
}
