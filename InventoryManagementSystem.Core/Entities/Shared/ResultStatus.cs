using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.Entities.Shared
{
    public class ResultStatus
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }

        private ResultStatus(bool isSuccess, string message = null)
        {
            Success = isSuccess;
            Message = message;
        }

        public static ResultStatus Successfuly() => new ResultStatus(true);
        public static ResultStatus Failure(string message) => new ResultStatus(false, message);
    }
}
