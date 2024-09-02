using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.Exceptions
{
    public class AppException : Exception
    {
        public AppException(string message, Exception innerException = null)
            : base(message, innerException)
        {
        }

        public AppException(string message, Exception innerException = null, string errorType = null)
            : base(message, innerException)
        {
            ErrorType = errorType;
        }

        public string ErrorType { get; }
    }
}
