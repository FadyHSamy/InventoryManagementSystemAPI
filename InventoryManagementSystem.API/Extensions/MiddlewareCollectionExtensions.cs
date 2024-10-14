using InventoryManagementSystem.API.Middlewares;

namespace InventoryManagementSystem.API.Extensions
{
    public static class MiddlewareCollectionExtensions
    {
        public static void AddCustomMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ApiResponseMiddleware>();
            app.UseMiddleware<CustomExceptionsMiddleware>();
            app.UseMiddleware<TransactionMiddleware>();
            app.UseMiddleware<AuthorizationMiddleware>();
        }
    }
}
