using Azure;
using InventoryManagementSystem.Core.Entities.Shared;
using InventoryManagementSystem.Core.Utilities.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;

namespace InventoryManagementSystem.API.Middlewares
{
    public class ApiResponseMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiResponseMiddleware> _logger;

        public ApiResponseMiddleware(RequestDelegate next, ILogger<ApiResponseMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                context.Response.Body = memoryStream;
                try
                {
                    await _next(context);

                    memoryStream.Seek(0, SeekOrigin.Begin);
                    context.Response.ContentType = "application/json";
                    string responseBody = await new StreamReader(memoryStream).ReadToEndAsync();
                    context.Response.Body = originalBodyStream;
                    string result;

                    if (context.Response.StatusCode >= 200 && context.Response.StatusCode < 300)
                    {
                        result = await HandleSuccessResponseAsync(responseBody, context);
                    }
                    else
                    {
                        result = await HandleFailureResponseAsync(responseBody, context);

                    }
                    await context.Response.WriteAsync(result);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An unhandled exception occurred.");
                    await HandleExceptionResponseAsync(await new StreamReader(memoryStream).ReadToEndAsync(), context, ex);
                }
                finally
                {
                    // Copy the contents of the new memory stream (responseBody) to the original stream
                    await memoryStream.CopyToAsync(originalBodyStream);
                }
            }

        }

        private async Task<string> HandleSuccessResponseAsync(string responseBody, HttpContext context)
        {
            try
            {
                var existingApiResponse = JsonConvert.DeserializeObject<ApiResponse<object>>(responseBody);
                if (existingApiResponse != null && existingApiResponse.isSuccess)
                {
                    return responseBody;
                }
            }
            catch (JsonException)
            {
            }

            ApiResponse<object> apiResponse = new ApiResponse<object>
            {
                isSuccess = true,
                message = "Request completed successfully.",
                data = Helpers.IsEmptyString(responseBody) ? null : JsonConvert.DeserializeObject<object>(responseBody),
                requestApiUrl = context.Request.GetDisplayUrl()
            };

            return JsonConvert.SerializeObject(apiResponse);

        }

        private async Task<string> HandleFailureResponseAsync(string responseBody, HttpContext context)
        {
            try
            {
                ApiResponse<object> existingApiResponse = JsonConvert.DeserializeObject<ApiResponse<object>>(responseBody)!;
                if (existingApiResponse != null && !existingApiResponse.isSuccess)
                {
                    return responseBody;
                }
            }
            catch (JsonException)
            {
            }

            ApiResponse<object> apiResponse = new ApiResponse<object>
            {
                isSuccess = false,
                message = "An error occurred.",
                data = null,
                requestApiUrl = context.Request.GetDisplayUrl()
            };
            return JsonConvert.SerializeObject(apiResponse);
            
        }

        private async Task HandleExceptionResponseAsync(string responseBody, HttpContext context, Exception ex)
        {
            context.Response.StatusCode = 500;
            ApiResponse<object> apiResponse = new ApiResponse<object>
            {
                isSuccess = false,
                message = "An internal server error occurred.",
                data = null,
                requestApiUrl = context.Request.GetDisplayUrl()
            };

            string result = JsonConvert.SerializeObject(apiResponse);
            await context.Response.WriteAsync(result);
        }
    }
}
