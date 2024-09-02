using InventoryManagementSystem.Core.Interfaces;
using InventoryManagementSystem.Infrastructure.Context;
using InventoryManagementSystem.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
