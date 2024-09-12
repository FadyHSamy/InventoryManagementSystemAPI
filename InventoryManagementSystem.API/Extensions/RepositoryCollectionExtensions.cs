using InventoryManagementSystem.Core.Interfaces.Repositories.AllUserIRepository;
using InventoryManagementSystem.Infrastructure.Repositories.AllUserRepository;

namespace InventoryManagementSystem.API.Extensions
{
    public static class RepositoryCollectionExtensions
    {
        public static void AddCustomRepository(this IServiceCollection services)
        {
            // Register your services here
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserStatusRepository, UserStatusRepository>();
            services.AddScoped<IUserRolesRepository, UserRoleRepository>();


        }
    }
}
