using InventoryManagementSystem.Core.Entities.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;

namespace InventoryManagementSystem.API.Middlewares
{
    public class ResponseWrapper
    {
        private readonly RequestDelegate _next;

        public ResponseWrapper(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;

            using (var memoryStream = new MemoryStream())
            {
                context.Response.Body = memoryStream;

                try
                {
                    await _next(context); // Invoke the next middleware in the pipeline.

                    memoryStream.Seek(0, SeekOrigin.Begin);
                    object responseBody = await new StreamReader(memoryStream).ReadToEndAsync();
                    var statusCode = (HttpStatusCode)context.Response.StatusCode;
                    context.Response.Body = originalBodyStream;

                    var result = (statusCode == HttpStatusCode.OK)
                        ? ApiWrapperResponse.CreateResponseObject(statusCode, responseBody, null)
                        : ApiWrapperResponse.CreateResponseObject(statusCode, null, CleanErrorMessage(responseBody.ToString()));

                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
                }
                catch (Exception ex)
                {
                    var errorResponse = ApiWrapperResponse.CreateResponseObject(HttpStatusCode.InternalServerError, null, ex.Message);
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
                }
                finally
                {
                    context.Response.Body = originalBodyStream; // Restore the original stream.
                }
            }
        }
        public static string CleanErrorMessage(string message)
        {
            return message.Replace("\\\"", "\"").Trim('"');
        }
    }
}
