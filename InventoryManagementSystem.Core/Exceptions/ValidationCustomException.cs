using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.Exceptions
{
    public class ValidationCustomException : Exception
    {
        // Parameterless constructor
        public ValidationCustomException()
        {
        }

        public ValidationCustomException(string message)
            : base(message)
        {
        }

        public ValidationCustomException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
