using InventoryManagementSystem.Core.Interfaces.Services;
using InventoryManagementSystem.Core.Mappers;
using InventoryManagementSystem.Core.Services;
using InventoryManagementSystem.Infrastructure.Context;

namespace InventoryManagementSystem.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            // Register your services here
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<DapperContext>();

            services.AddAutoMapper(typeof(UserProfile));

        }
    }
}
