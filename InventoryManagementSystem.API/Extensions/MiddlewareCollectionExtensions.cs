﻿using InventoryManagementSystem.API.Middlewares;

namespace InventoryManagementSystem.API.Extensions
{
    public static class MiddlewareCollectionExtensions
    {
        public static void AddCustomMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ResponseWrapper>();
            app.UseMiddleware<CustomMiddleware>();
        }
    }
}
