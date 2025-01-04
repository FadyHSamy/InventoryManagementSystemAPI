using InventoryManagementSystem.Core.Exceptions;
using InventoryManagementSystem.Core.Interfaces.Services.AllJwtServices;
using Microsoft.AspNetCore.Authorization;

namespace InventoryManagementSystem.API.Middlewares
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IJwtService jwtService)
        {
            var endpoint = context.GetEndpoint();

            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
            {
                // Skip authorization if AllowAnonymous is applied
                await _next(context);
                return;
            }

            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token == null)
            {
                throw new AuthException("Unauthorized: No token provided.");
            }

            var principal = jwtService.ValidateRefreshToken(token);
            if (principal == null)
            {
                throw new AuthException("Unauthorized: Invalid token.");
            }

            // Attach user to context on successful JWT validation
            context.User = principal;

            await _next(context);
        }
    }
}
