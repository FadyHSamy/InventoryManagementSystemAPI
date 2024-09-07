using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.Entities.Shared
{
    public class ApiWrapperResponse
    {
        public bool Success { get; private set; }
        public object Result { get; private set; }
        public string ErrorMessage { get; private set; }

        public static ApiWrapperResponse Successfully(object result = null) => new ApiWrapperResponse(HttpStatusCode.OK, result, null);
        public static ApiWrapperResponse Failure(string message) => new ApiWrapperResponse(HttpStatusCode.BadRequest, null, message);

        public static ApiWrapperResponse CreateResponseObject(HttpStatusCode statusCode, object result = null, string errorMessage = null)
        {
            return new ApiWrapperResponse(statusCode, result, errorMessage);
        }

        protected ApiWrapperResponse(HttpStatusCode statusCode, object result = null, string errorMessage = null)
        {
            Success = statusCode == HttpStatusCode.OK;
            Result = Success ? result : null;
            ErrorMessage = Success ? null : errorMessage;
        }
    }
}
