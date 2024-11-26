using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.Entities.Shared
{
    public class ApiResponse<T>
    {
        public bool isSuccess { get; set; }
        public string? message { get; set; }
        public T? data { get; set; }
        public string requestApiUrl { get; set; }
    }
}
