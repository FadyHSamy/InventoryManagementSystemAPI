using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static InventoryManagementSystem.Core.Validators.InventoryValidations;
using static InventoryManagementSystem.Core.Validators.ProductValidations;

namespace InventoryManagementSystem.Core.DTOs.ProductDto
{
    public class InsertProductRequest
    {
        [ProductNameValidation]
        public string ProductName { get; set; }

        [ProductDescriptionValidation(false)]
        public string? ProductDescription { get; set; }

        [ProductPriceValidation]
        public decimal ProductPrice { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [StockQuantityValidation]
        public int StockQuantity { get; set; }
    }
}
