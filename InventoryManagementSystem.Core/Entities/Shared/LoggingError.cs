﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace InventoryManagementSystem.Core.Entities.Shared
{
    public class LoggingError
    {
        public int LogID { get; set; } // Unique identifier for each log entry
        public DateTime Timestamp { get; set; } // Timestamp when the log entry was created
        public string LogLevel { get; set; } // Log level (e.g., Error, Warning, Info, Debug)
        public string Message { get; set; } // The log message
        public string Exception { get; set; } // Exception details, if applicable
        public string InnerException { get; set; } // Inner exception details, if applicable
        public string StackTrace { get; set; } // Stack trace of the error, if available
        public string ApplicationName { get; set; } // Name of the application or module logging the entry
        public string UserID { get; set; } // User identifier, if available (e.g., username)
        public string MachineName { get; set; } // Name of the machine/server where the log was generated
        public string Source { get; set; } // Source of the log (e.g., method, component)
        public string RequestID { get; set; } // Correlation ID to trace specific requests
        public string AdditionalInfo { get; set; } // Any additional information, serialized as JSON or key-value pairs
    }
}
