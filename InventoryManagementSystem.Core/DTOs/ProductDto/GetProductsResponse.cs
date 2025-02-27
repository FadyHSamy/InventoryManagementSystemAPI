﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.DTOs.ProductDto
{
    public class GetProductsResponse
    {
        public decimal ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
