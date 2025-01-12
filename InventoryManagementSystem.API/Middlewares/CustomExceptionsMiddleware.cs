using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Azure.Core;
using InventoryManagementSystem.Core.Entities.Shared;
using InventoryManagementSystem.Core.Exceptions;
using InventoryManagementSystem.Core.Interfaces.Services.AllLoggingIServices;
using InventoryManagementSystem.Core.Utilities.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace InventoryManagementSystem.API.Middlewares
{
    public class CustomExceptionsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;

        public CustomExceptionsMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                await LogErrorAsync(context, error);
                context.Response.ContentType = "application/json";

                switch (error)
                {
                    case ValidationCustomException validationEx:
                        await HandleExceptionAsync(context, validationEx.Message, HttpStatusCode.Forbidden);
                        break;
                    case DatabaseException dbEx:
                        await HandleExceptionAsync(context, dbEx.Message, HttpStatusCode.InternalServerError);
                        break;
                    case NotFoundException notFoundEx:
                        await HandleExceptionAsync(context, notFoundEx.Message, HttpStatusCode.NotFound);
                        break;
                    case AuthException authException:
                        await HandleExceptionAsync(context, authException.Message, HttpStatusCode.Unauthorized);
                        break;
                    case BadRequestException badRequestException:
                        await HandleExceptionAsync(context, badRequestException.Message, HttpStatusCode.BadRequest);
                        break;
                    case ValidationException validationException:
                        await HandleExceptionAsync(context, validationException.Message, HttpStatusCode.BadRequest);
                        break;
                    default:
                        await HandleExceptionAsync(context, "An unhandled exception occurred.", HttpStatusCode.InternalServerError);
                        break;
                }
            }

        }
        private async Task LogErrorAsync(HttpContext context, Exception exception)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var scopedService = scope.ServiceProvider.GetRequiredService<ILoggingServices>();

                string message = exception.Message;
                if (message == "Unauthorized: Invalid token.")
                {
                    return;
                }

                var username = context.User?.FindFirst("username")?.Value ?? "Anonymous";

                var request = context.Request;
                var method = request.Method;
                var requestUrl = request.GetDisplayUrl();

                var loggingError = new LoggingError
                {
                    Message = message,
                    Exception = JsonConvert.SerializeObject(exception),
                    ApplicationName = "InventoryManagementSystem.API",
                    UserID = username,
                    Source = exception.Source,
                    AdditionalInfo = $"{method} --> {requestUrl}",
                };

                await scopedService.InsertLoggingError(loggingError);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, string errorMessage, HttpStatusCode statusCode)
        {
            context.Response.StatusCode = (int)statusCode;

            ApiResponse<object> response = ApiResponseHelper.Failure<object>(context.Request, errorMessage);
            string jsonResponse = JsonConvert.SerializeObject(response);

            return context.Response.WriteAsync(jsonResponse);
        }
    }
}