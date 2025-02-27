﻿using InventoryManagementSystem.Core.Entities.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.Utilities.Helpers
{
    public static class ApiResponseHelper
    {
        public static ApiResponse<T> Success<T>(HttpRequest request, string? message = null, T? data = default)
        {
            return new ApiResponse<T>
            {
                isSuccess = true,
                message = message,
                data = data,
                requestApiUrl = request.GetDisplayUrl()
            };
        }

        public static ApiResponse<T> Failure<T>(HttpRequest request, string? message)
        {
            return new ApiResponse<T>
            {
                isSuccess = false,
                message = message,
                data = default,
                requestApiUrl = request.GetDisplayUrl()
            };
        }
    }
}
