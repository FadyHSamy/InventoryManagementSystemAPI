using InventoryManagementSystem.Core.Entities.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
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
                    string responseBody = await new StreamReader(memoryStream).ReadToEndAsync();
                    context.Response.Body = originalBodyStream;

                    object objResult = HandlingResponseBody(responseBody);
                    
                    var statusCode = (HttpStatusCode)context.Response.StatusCode;
                    var result = (statusCode == HttpStatusCode.OK)
                        ? ApiWrapperResponse.CreateResponseObject(statusCode, objResult, null)
                        : ApiWrapperResponse.CreateResponseObject(statusCode, null, objResult.ToString());

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
        private object HandlingResponseBody(string stri)
        {
            if (IsValidJson(stri))
            {
                return JsonConvert.DeserializeObject(stri);
            }
            return stri;
        }
        private static bool IsValidJson(string strInput)
        {
            if (string.IsNullOrWhiteSpace(strInput)) { return false; }
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
