using InventoryManagementSystem.Core.Interfaces.Repositories.AllAuthRepository;
using InventoryManagementSystem.Core.Interfaces.Repositories.AllCategoryIRepository;
using InventoryManagementSystem.Core.Interfaces.Repositories.AllInventoryIRepository;
using InventoryManagementSystem.Core.Interfaces.Repositories.AllLoggingIRepository;
using InventoryManagementSystem.Core.Interfaces.Repositories.AllProductIRepository;
using InventoryManagementSystem.Core.Interfaces.Repositories.AllUserIRepository;
using InventoryManagementSystem.Infrastructure.Repositories.AllAuthRepository;
using InventoryManagementSystem.Infrastructure.Repositories.AllCategoryRepository;
using InventoryManagementSystem.Infrastructure.Repositories.AllInventoryRepository;
using InventoryManagementSystem.Infrastructure.Repositories.AllLoggingRepository;
using InventoryManagementSystem.Infrastructure.Repositories.AllProductRepository;
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
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IInventoryRepository, InventoryRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();


        }
    }
}
