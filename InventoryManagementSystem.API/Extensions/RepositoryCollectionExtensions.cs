using InventoryManagementSystem.Core.Interfaces.Repositories;
using InventoryManagementSystem.Infrastructure.Repositories;

namespace InventoryManagementSystem.API.Extensions
{
    public static class RepositoryCollectionExtensions
    {
        public static void AddCustomRepository(this IServiceCollection services)
        {
            // Register your services here
            services.AddScoped<IUserRepository, UserRepository>();


        }
    }
}
