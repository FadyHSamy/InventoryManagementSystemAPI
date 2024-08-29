using InventoryManagementSystem.Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            // Register your services here
            //services.AddScoped<IProductRepository, ProductRepository>();

            // Register DapperContext
            services.AddScoped<DapperContext>();

        }
    }
}
