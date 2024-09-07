using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Configuration
{
    public class DatabaseSettings
    {
        public string DefaultConnection { get; set; } = string.Empty;
    }
}
