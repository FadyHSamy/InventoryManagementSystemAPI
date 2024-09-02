using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using InventoryManagementSystem.Core.Exceptions;
using Microsoft.AspNetCore.Http;

namespace InventoryManagementSystem.API.Middlewares
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (AppException ex)
            {
                await HandleAppExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleGeneralExceptionAsync(context, ex);
            }
        }

        private Task HandleAppExceptionAsync(HttpContext context, AppException ex)
        {
            context.Response.ContentType = "application/json";
            if (ex.ErrorType == "Validation")
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
            else if (ex.ErrorType == "NotFound")
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
            }
            else if (ex.ErrorType == "Unauthorized")
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
            var response = new { message = ex.Message };
            return context.Response.WriteAsJsonAsync(response);
        }

        private Task HandleGeneralExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var response = new { message = "An unexpected error occurred." };
            return context.Response.WriteAsJsonAsync(response);
        }
    }
}