using InventoryManagementSystem.Core.Entities.Shared;
using InventoryManagementSystem.Core.Exceptions;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace InventoryManagementSystem.API.Middlewares
{
   public class RequestValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiResponseMiddleware> _logger;
        public RequestValidationMiddleware(RequestDelegate next, ILogger<ApiResponseMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                if (context.Request.Method == HttpMethods.Post)
                {
                    context.Request.EnableBuffering();

                    string requestBody;
                    using (var reader = new StreamReader(context.Request.Body, leaveOpen: true))
                    {
                        requestBody = await reader.ReadToEndAsync();
                    }

                    var requestDto = DeserializeRequestBody(requestBody);
                    if (requestDto == null)
                    {
                        throw new ValidationException("Request body cannot be null or empty.");
                    }

                    var realDto = CreateRealDtoFromContext(context);

                    if (!AreDtosEqual(realDto, requestDto))
                    {
                        throw new ValidationException("Request DTO does not match the expected DTO.");
                    }

                    context.Request.Body.Position = 0;

                }
            }
            catch (Exception ex)
            {
                throw new BadRequestException($"Internal server error: {ex.Message}");
            }

            await _next(context);
        }

        private object DeserializeRequestBody(string requestBody)
        {
            return JsonSerializer.Deserialize<object>(requestBody);
        }
        private object CreateRealDtoFromContext(HttpContext context)
        {
            var dtoType = GetExpectedDtoType(context);
            if (dtoType == null)
            {
                throw new InvalidOperationException("DTO type could not be determined.");
            }
            var dto = Activator.CreateInstance(dtoType);

            foreach (var property in dtoType.GetProperties())
            {
                var claimValue = context.User.FindFirst(property.Name)?.Value;
                if (claimValue != null)
                {
                    property.SetValue(dto, Convert.ChangeType(claimValue, property.PropertyType));
                }
            }

            //foreach (var property in dtoType.GetProperties())
            //{
            //    var claim = context.User.FindFirst(property.Name);
            //    if (claim != null)
            //    {
            //        try
            //        {
            //            var value = Convert.ChangeType(claim.Value, property.PropertyType);
            //            property.SetValue(dto, value);
            //        }
            //        catch (Exception ex)
            //        {
            //            throw new InvalidOperationException(
            //                $"Error setting property {property.Name} on {dtoType.Name}: {ex.Message}", ex);
            //        }
            //    }
            //}

            return dto;
        }
        private bool AreDtosEqual(object dto1, object dto2)
        {
            if (dto1 == null || dto2 == null)
            {
                return false;
            }

            foreach (var property in dto1.GetType().GetProperties())
            {
                var value1 = property.GetValue(dto1);
                var value2 = property.GetValue(dto2);

                if (value1 == null && value2 == null)
                {
                    continue;
                }

                if (value1 == null || value2 == null || !value1.Equals(value2))
                {
                    return false;
                }
            }

            return true;
        }
        private Type GetExpectedDtoType(HttpContext context)
        {
            var routeData = context.GetRouteData();
            var test = context.GetEndpoint();
       
            var test4 = test.RequestDelegate.Target;

            if (routeData?.Values.TryGetValue("controller", out var controllerName) == true &&
                routeData.Values.TryGetValue("action", out var actionName) == true)
            {
                string namespaceName = "InventoryManagementSystem.Core.DTOs";
                string typeName = $"{namespaceName}.{controllerName}{actionName}RequestDto";

                // Use the appropriate assembly to find the type
                var dtoType = Type.GetType(typeName) ??
                              AppDomain.CurrentDomain.GetAssemblies()
                                  .Select(assembly => assembly.GetType(typeName))
                                  .FirstOrDefault(t => t != null);

                if (dtoType != null)
                {
                    return dtoType;
                }
            }

            throw new InvalidOperationException("Unable to determine the expected DTO type from route data.");
        }

    }
}
