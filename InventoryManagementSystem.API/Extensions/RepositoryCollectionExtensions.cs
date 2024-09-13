using InventoryManagementSystem.Core.Interfaces.Repositories.AllCategoryIRepository;
using InventoryManagementSystem.Core.Interfaces.Repositories.AllLoggingIRepository;
using InventoryManagementSystem.Core.Interfaces.Repositories.AllUserIRepository;
using InventoryManagementSystem.Infrastructure.Repositories.AllCategoryRepository;
using InventoryManagementSystem.Infrastructure.Repositories.AllLoggingRepository;
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
            services.AddScoped<ILoggingRepository, LoggingRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();


        }
    }
}
