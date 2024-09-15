﻿using InventoryManagementSystem.Core.Interfaces.Services.AllCategoryIServices;
using InventoryManagementSystem.Core.Interfaces.Services.AllLoggingIServices;
using InventoryManagementSystem.Core.Interfaces.Services.AllProductIServices;
using InventoryManagementSystem.Core.Interfaces.Services.AllUserIServices;
using InventoryManagementSystem.Core.Mappers.UserMappers;
using InventoryManagementSystem.Core.Services.AllCategoryServices;
using InventoryManagementSystem.Core.Services.AllLoggingServices;
using InventoryManagementSystem.Core.Services.AllProductServices;
using InventoryManagementSystem.Core.Services.AllUserServices;
using InventoryManagementSystem.Infrastructure.Context;
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

            services.AddSingleton(Log.Logger);

            services.AddScoped<DapperContext>();

            services.AddAutoMapper(typeof(UserProfile));

        }
    }
}
