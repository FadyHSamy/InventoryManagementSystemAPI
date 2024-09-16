using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.DTOs.InventoryDto
{
    public class GetProductInventoryResponse
    {
        public int InventoryId { get; set; }
        public int StockQuantity { get; set; }
        public int ProductId { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
