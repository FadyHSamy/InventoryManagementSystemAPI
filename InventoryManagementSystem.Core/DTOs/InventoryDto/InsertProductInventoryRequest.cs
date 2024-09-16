using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static InventoryManagementSystem.Core.Validators.InventoryValidations;

namespace InventoryManagementSystem.Core.DTOs.InventoryDto
{
    public class InsertProductInventoryRequest
    {
        [ProductIdValidation]
        public decimal ProductId { get; set; }
        [StockQuantityValidation]
        public int StockQuantity { get; set; }
    }
}
