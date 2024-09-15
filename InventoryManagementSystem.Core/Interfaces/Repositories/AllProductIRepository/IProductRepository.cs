using InventoryManagementSystem.Core.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.Interfaces.Repositories.AllProductIRepository
{
    public interface IProductRepository
    {
        Task<Product> GetProduct(int ProductId);
        Task<List<Product>> GetProducts();
        Task InsertProduct(Product Product);
        Task DeleteProduct(int ProductId);
    }
}
