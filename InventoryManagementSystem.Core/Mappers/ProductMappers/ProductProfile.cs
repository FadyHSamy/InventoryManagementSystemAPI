using AutoMapper;
using InventoryManagementSystem.Core.DTOs.CategoryDto;
using InventoryManagementSystem.Core.DTOs.ProductDto;
using InventoryManagementSystem.Core.Entities.Category;
using InventoryManagementSystem.Core.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.Mappers.ProductMappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<GetProductResponse, Product>()
                .ReverseMap();

            CreateMap<GetProductsResponse, Product>()
                .ReverseMap();

            CreateMap<InsertProductRequest, Product>()
                .ReverseMap();
        }
    }
}
