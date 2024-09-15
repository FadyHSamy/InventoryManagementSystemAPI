using InventoryManagementSystem.Core.Entities.Shared;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.Interfaces.Services.AllLoggingIServices
{
    public interface ILoggingServices
    {
        Task InsertLoggingError(LoggingError loggingError);
    }
}
