using Azure.Core;
using InventoryManagementSystem.Core.Entities.Shared;
using InventoryManagementSystem.Core.Exceptions;
using InventoryManagementSystem.Core.Interfaces.Services.AllLoggingIServices;
using Newtonsoft.Json;
using System.Text;

namespace InventoryManagementSystem.API.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;


        public LoggingMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var loggingServices = scope.ServiceProvider.GetRequiredService<ILoggingServices>();
                try
                {
                    await _next(context);
                }
                catch (Exception ex)
                {
                    if (context.Response.StatusCode == 500)
                    {
                        await LogErrorExceptionAsync(context, ex, loggingServices);
                    }
                }
            }
        }

        private async Task LogErrorExceptionAsync(HttpContext context, Exception exception, ILoggingServices loggingServices)
        {
            var request = context.Request;
            var loggingError = new LoggingError
            {
                AdditionalInfo = $"{request.Method} {request.Path}",
                ApplicationName = "Api",
                Exception = JsonConvert.SerializeObject(exception),
                InnerException = JsonConvert.SerializeObject(exception.InnerException),
                LogLevel = "Error",
                MachineName = Environment.MachineName,
                Message = exception.Message,
                RequestID = context.TraceIdentifier,
                Source = exception.Source,
                StackTrace = exception.StackTrace,
                UserID = context.User.Identity?.Name ?? "Anonymous"
            };

            await loggingServices.InsertLoggingError(loggingError);
        }
    }
}
