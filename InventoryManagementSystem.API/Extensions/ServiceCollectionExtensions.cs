using InventoryManagementSystem.Core.Interfaces.Repositories.AllSharedIRepository;
using InventoryManagementSystem.Core.Interfaces.Services.AllAuthServices;
using InventoryManagementSystem.Core.Interfaces.Services.AllCategoryIServices;
using InventoryManagementSystem.Core.Interfaces.Services.AllInventoryIServices;
using InventoryManagementSystem.Core.Interfaces.Services.AllJwtServices;
using InventoryManagementSystem.Core.Interfaces.Services.AllLoggingIServices;
using InventoryManagementSystem.Core.Interfaces.Services.AllProductIServices;
using InventoryManagementSystem.Core.Interfaces.Services.AllUserIServices;
using InventoryManagementSystem.Core.Mappers.UserMappers;
using InventoryManagementSystem.Core.Services.AllCategoryServices;
using InventoryManagementSystem.Core.Services.AllInventoryServices;
using InventoryManagementSystem.Core.Services.AllLoggingServices;
using InventoryManagementSystem.Core.Services.AllProductServices;
using InventoryManagementSystem.Core.Services.AllUserServices;
using InventoryManagementSystem.Infrastructure.Context;
using InventoryManagementSystem.Infrastructure.Repositories.AllSharedRepository;
using InventoryManagementSystem.Infrastructure.Services;
using Serilog;

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
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ILoggingServices, LoggingServices>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IInventoryServices, InventoryServices>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtService, JwtService>();

            services.AddSingleton(Log.Logger);

            services.AddScoped<IDapperContext,DapperContext>();

            services.AddAutoMapper(typeof(UserProfile));

            services.AddSingleton(provider => provider);

        }
    }
}
