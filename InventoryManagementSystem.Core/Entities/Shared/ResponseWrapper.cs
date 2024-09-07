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
        public bool Success { get; set; }

        public object Result { get; set; }

        public string ErrorMessage { get; set; }


        public static ApiWrapperResponse CreateResponseObject(HttpStatusCode statusCode, object result = null, string errorMessage = null)
        {
            return new ApiWrapperResponse(statusCode, result, errorMessage);
        }

        protected ApiWrapperResponse(HttpStatusCode statusCode, object result = null, string errorMessage = null)
        {
            Success = (int)statusCode == 200 ? true : false;
            Result = result;
            ErrorMessage = errorMessage;
        }
    }
}
