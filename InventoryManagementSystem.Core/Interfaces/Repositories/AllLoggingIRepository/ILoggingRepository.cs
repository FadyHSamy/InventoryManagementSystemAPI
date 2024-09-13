using InventoryManagementSystem.Core.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.Interfaces.Repositories.AllLoggingIRepository
{
    public interface ILoggingRepository
    {
        Task InsertLoggingError(LoggingError loggingError);
    }
}
