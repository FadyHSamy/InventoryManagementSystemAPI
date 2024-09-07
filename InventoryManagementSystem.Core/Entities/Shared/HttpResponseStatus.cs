using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.Entities.Shared
{
    public class HttpResponseStatus
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }

        private HttpResponseStatus(bool isSuccess, string message = null)
        {
            Success = isSuccess;
            Message = message;
        }

        public static HttpResponseStatus Successfully() => new HttpResponseStatus(true);
        public static HttpResponseStatus Failure(string message) => new HttpResponseStatus(false, message);
    }
}
