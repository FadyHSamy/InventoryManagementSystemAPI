using InventoryManagementSystem.Core.DTOs.ProductDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.Interfaces.Services.AllProductIServices
{
    public interface IProductService
    {
        Task<GetProductResponse> GetProduct(int ProductId);
        Task<List<GetProductsResponse>> GetProducts();
        Task InsertProduct(InsertProductRequest insertProductRequest);
        Task DeleteProduct(int ProductId);        
    }
}
