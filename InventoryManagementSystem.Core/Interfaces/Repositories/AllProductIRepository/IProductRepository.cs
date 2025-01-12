using InventoryManagementSystem.Core.DTOs.ProductDto;
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
        Task<GetProductResponse> GetProduct(int ProductId);
        Task<List<GetProductsResponse>> GetProducts();
        Task<int> InsertProduct(Product Product);
        Task DeleteProduct(int ProductId);
    }
}
