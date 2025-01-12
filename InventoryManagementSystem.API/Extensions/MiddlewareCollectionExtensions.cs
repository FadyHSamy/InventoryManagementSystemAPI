using InventoryManagementSystem.API.Middlewares;

namespace InventoryManagementSystem.API.Extensions
{
    public static class MiddlewareCollectionExtensions
    {
        public static void AddCustomMiddleware(this IApplicationBuilder app)
        {
            // Step 1: Add Response Formatting middleware first
            app.UseMiddleware<ApiResponseMiddleware>();

            // Step 2: Add Exception Handling middleware next
            app.UseMiddleware<CustomExceptionsMiddleware>();

            // Step 3: Add Transaction Handling middleware
            app.UseMiddleware<TransactionMiddleware>();

            // Step 4: Add Authorization middleware last
            app.UseMiddleware<AuthorizationMiddleware>();

            // Step 5: Add check for request validation
          //  app.UseMiddleware<RequestValidationMiddleware>();
        }
    }
}
