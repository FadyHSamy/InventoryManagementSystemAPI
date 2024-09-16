using AutoMapper;
using InventoryManagementSystem.Core.DTOs.CategoryDto;
using InventoryManagementSystem.Core.DTOs.UserDto;
using InventoryManagementSystem.Core.Entities.Category;
using InventoryManagementSystem.Core.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.Mappers.CategoryMappers
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile() 
        {
            CreateMap<GetCategoriesResponse, Category>()
                .ReverseMap();

            CreateMap<GetCategoryResponse, Category>()
                .ReverseMap();
        }
    }
}
