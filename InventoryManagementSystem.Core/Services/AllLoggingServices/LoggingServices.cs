using InventoryManagementSystem.Core.Entities.Shared;
using InventoryManagementSystem.Core.Interfaces.Repositories.AllLoggingIRepository;
using InventoryManagementSystem.Core.Interfaces.Services.AllLoggingIServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.Services.AllLoggingServices
{
    public class LoggingServices : ILoggingServices
    {
        private readonly ILoggingRepository _loggingRepository;
        public LoggingServices(ILoggingRepository loggingRepository)
        {
            _loggingRepository = loggingRepository;
        }

        public async Task InsertLoggingError(LoggingError loggingError)
        {
            await _loggingRepository.InsertLoggingError(loggingError);
        }
    }
}
