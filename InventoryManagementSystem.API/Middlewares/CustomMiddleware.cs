using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using InventoryManagementSystem.Core.Entities.Shared;
using InventoryManagementSystem.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
            catch (ValidationCustomException ex)
            {
                await HandleValidationExceptionAsync(context, ex);
            }
            catch (DatabaseException ex)
            {
                await HandleDatabaseExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleGeneralExceptionAsync(context, ex);
            }
        }
        private Task HandleGeneralExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var response = string.IsNullOrEmpty(ex.Message) ? "An unexpected error occurred." : ex.Message;

            return context.Response.WriteAsJsonAsync(response);
        }

        private Task HandleValidationExceptionAsync(HttpContext context, ValidationCustomException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return context.Response.WriteAsJsonAsync(exception.Message);
        }
        private Task HandleDatabaseExceptionAsync(HttpContext context, DatabaseException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return context.Response.WriteAsJsonAsync(exception.Message);
        }

    }
}