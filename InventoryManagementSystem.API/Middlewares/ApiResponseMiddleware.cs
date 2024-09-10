using InventoryManagementSystem.Core.Entities.Shared;
using InventoryManagementSystem.Core.Utilities.Helpers;
using Microsoft.AspNetCore.Http.Extensions;
using System.Text.Json;

namespace InventoryManagementSystem.API.Middlewares
{
    public class ApiResponseMiddleware
    {
        private readonly RequestDelegate _next;
        //private readonly ILogger<ApiResponseMiddleware> _logger;

        public ApiResponseMiddleware(RequestDelegate next, ILogger<ApiResponseMiddleware> logger)
        {
            _next = next;
            //_logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Capture the original response stream
            var originalBodyStream = context.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                // Replace the response stream with our custom stream
                context.Response.Body = responseBody;

                try
                {
                    // Proceed with the next middleware in the pipeline
                    await _next(context);

                    // Handle the response based on the status code
                    if (context.Response.StatusCode >= 200 && context.Response.StatusCode < 300)
                    {
                        await HandleSuccessResponseAsync(context);
                    }
                    else
                    {
                        await HandleFailureResponseAsync(context);
                    }
                }
                catch (Exception ex)
                {
                    //_logger.LogError(ex, "An unhandled exception occurred.");
                    await HandleExceptionResponseAsync(context, ex);
                }
                finally
                {
                    // Copy the contents of the new memory stream (responseBody) to the original stream
                    await responseBody.CopyToAsync(originalBodyStream);
                }
            }
        }

        private async Task HandleSuccessResponseAsync(HttpContext context)
        {
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();

            try
            {
                var existingApiResponse = JsonSerializer.Deserialize<ApiResponse<object>>(responseBody);
                if (existingApiResponse != null && existingApiResponse.IsSuccess)
                {
                    // If the response is already a valid ApiResponse, don't overwrite it
                    context.Response.Body.Seek(0, SeekOrigin.Begin);
                    await context.Response.WriteAsync(responseBody);
                    return;
                }
            }
            catch (JsonException)
            {
                // If not a valid ApiResponse, continue to wrap it
            }

            // Deserialize the original response data if needed
            if (!Helpers.IsEmptyString(responseBody))
            {
                var data = JsonSerializer.Deserialize<object>(responseBody);
            }

            var apiResponse = new ApiResponse<object>
            {
                IsSuccess = true,
                Message = "Request completed successfully.",
                Data = Helpers.IsEmptyString(responseBody) ? null : JsonSerializer.Deserialize<object>(responseBody),
                RequestApiUrl = context.Request.GetDisplayUrl()
            };

            // Rewrite the response with the ApiResponse
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            context.Response.ContentType = "application/json";
            await JsonSerializer.SerializeAsync(context.Response.Body, apiResponse);
        }

        private async Task HandleFailureResponseAsync(HttpContext context)
        {
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();

            // Check if the response is already an ApiResponse
            try
            {
                var existingApiResponse = JsonSerializer.Deserialize<ApiResponse<object>>(responseBody);
                if (existingApiResponse != null && !existingApiResponse.IsSuccess)
                {
                    // If the response is already a valid ApiResponse with failure status, don't overwrite it
                    context.Response.Body.Seek(0, SeekOrigin.Begin);
                    await context.Response.WriteAsync(responseBody);
                    return;
                }
            }
            catch (JsonException)
            {
                // If not a valid ApiResponse, continue to wrap it
            }

            // Create a failure response with an optional error message from the response body
            var apiResponse = new ApiResponse<object>
            {
                IsSuccess = false,
                Message = "An error occurred.",
                Data = null,
                RequestApiUrl = context.Request.GetDisplayUrl()
            };

            // Rewrite the response with the ApiResponse
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            context.Response.ContentType = "application/json";
            await JsonSerializer.SerializeAsync(context.Response.Body, apiResponse);
        }

        private async Task HandleExceptionResponseAsync(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = 500;
            var apiResponse = new ApiResponse<object>
            {
                IsSuccess = false,
                Message = "An internal server error occurred.",
                Data = null,
                RequestApiUrl = context.Request.GetDisplayUrl()
            };

            context.Response.ContentType = "application/json";
            await JsonSerializer.SerializeAsync(context.Response.Body, apiResponse);
        }
    }
}
