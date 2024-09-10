using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Azure.Core;
using InventoryManagementSystem.Core.Entities.Shared;
using InventoryManagementSystem.Core.Exceptions;
using InventoryManagementSystem.Core.Utilities.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
                await ValidationException(context, ex);
            }
            catch (DatabaseException ex)
            {
                await DatabaseException(context, ex);
            }
            catch (NotFoundException ex)
            {
                await NotFoundException(context, ex);
            }
            catch (Exception ex)
            {
                await GeneralException(context, ex);
            }
        }

        public Task GeneralException(HttpContext context, Exception exception)
        {
            HttpStatusCode StatusCode = HttpStatusCode.InternalServerError;
            context.Response.StatusCode = (int)StatusCode;
            context.Response.ContentType = "application/json";
            ApiResponse<object> response = ApiResponseHelper.Failure<object>(context.Request, "An unhandled exception occurred.");
            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
        private Task ValidationException(HttpContext context, ValidationCustomException exception)
        {
            HttpStatusCode StatusCode = HttpStatusCode.Forbidden;
            context.Response.StatusCode = (int)StatusCode;
            context.Response.ContentType = "application/json";
            ApiResponse<object> response = ApiResponseHelper.Failure<object>(context.Request, exception.Message);
            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
        private Task DatabaseException(HttpContext context, DatabaseException exception)
        {
            HttpStatusCode StatusCode = HttpStatusCode.InternalServerError;
            context.Response.StatusCode = (int)StatusCode;
            context.Response.ContentType = "application/json";
            ApiResponse<object> response = ApiResponseHelper.Failure<object>(context.Request, exception.Message);
            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
        private Task NotFoundException(HttpContext context, NotFoundException exception)
        {
            HttpStatusCode StatusCode = HttpStatusCode.NotFound;
            context.Response.StatusCode = (int)StatusCode;
            context.Response.ContentType = "application/json";
            ApiResponse<object> response = ApiResponseHelper.Failure<object>(context.Request, exception.Message);
            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }

    }
}