using AutoMapper;
using InventoryManagementSystem.Core.DTOs.ProductDto;
using InventoryManagementSystem.Core.Entities.Product;
using InventoryManagementSystem.Core.Exceptions;
using InventoryManagementSystem.Core.Interfaces.Repositories.AllProductIRepository;
using InventoryManagementSystem.Core.Interfaces.Services.AllProductIServices;

namespace InventoryManagementSystem.Core.Services.AllProductServices
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task DeleteProduct(int ProductId)
        {
            try
            {
                Product product = await _productRepository.GetProduct(ProductId);
                if (product == null)
                {
                    throw new NotFoundException("No Product Exist With This Id");
                }
                await _productRepository.DeleteProduct(ProductId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetProductResponse> GetProduct(int ProductId)
        {
            try
            {
                if (ProductId == 0)
                {
                    throw new ValidationCustomException("ProductId Is Required");
                }
                Product product = await _productRepository.GetProduct(ProductId);
                if (product == null)
                {
                    throw new NotFoundException("No Product Exist With This Id");
                }
                GetProductResponse response = new()
                {
                    CategoryId = product.CategoryId,
                    ProductName = product.ProductName,
                    ProductDescription = product.ProductDescription,
                    ProductPrice = product.ProductPrice,
                    ProductId = product.ProductId
                };
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GetProductsResponse>> GetProducts()
        {
            try
            {
                List<Product> product = await _productRepository.GetProducts();
                List<GetProductsResponse> response = product.Select(product => new GetProductsResponse()
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    ProductDescription = product.ProductDescription,
                    ProductPrice = product.ProductPrice
                }).ToList();
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task InsertProduct(InsertProductRequest insertProductRequest)
        {
            try
            {
                Product product = _mapper.Map<Product>(insertProductRequest);
                await _productRepository.InsertProduct(product);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
