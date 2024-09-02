using InventoryManagementSystem.Core.Interfaces.Services;
using InventoryManagementSystem.Core.Services;
using InventoryManagementSystem.Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            // Register your services here
            services.AddScoped<IUserService, UserService>();

            // Register DapperContext
            services.AddScoped<DapperContext>();

        }
    }
}
