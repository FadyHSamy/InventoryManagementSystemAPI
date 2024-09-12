using InventoryManagementSystem.Core.Interfaces.Services.AllUserIServices;
using InventoryManagementSystem.Core.Mappers.UserMappers;
using InventoryManagementSystem.Core.Services.AllUserServices;
using InventoryManagementSystem.Infrastructure.Context;

namespace InventoryManagementSystem.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            // Register your services here
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserStatusService, UserStatusService>();
            services.AddScoped<IUserRoleService, UserRoleService>();

            services.AddScoped<DapperContext>();

            services.AddAutoMapper(typeof(UserProfile));

        }
    }
}
