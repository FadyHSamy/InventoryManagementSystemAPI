using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.DTOs.CategoryDto
{
    public class GetCategoriesResponse
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
