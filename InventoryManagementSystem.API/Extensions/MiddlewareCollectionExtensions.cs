namespace InventoryManagementSystem.API.Extensions
{
    public static class MiddlewareCollectionExtensions
    {
        public static void AddCustomMiddleware(this IServiceCollection services)
        {
            // Register your services here
            //services.AddScoped<IUserRepository, UserRepository>();

            // Register DapperContext
            //services.AddScoped<DapperContext>();

        }
    }
}
