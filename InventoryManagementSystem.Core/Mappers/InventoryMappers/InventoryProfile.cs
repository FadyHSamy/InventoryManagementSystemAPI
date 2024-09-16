using AutoMapper;
using InventoryManagementSystem.Core.DTOs.CategoryDto;
using InventoryManagementSystem.Core.DTOs.InventoryDto;
using InventoryManagementSystem.Core.Entities.Category;
using InventoryManagementSystem.Core.Entities.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.Mappers.InventoryMappers
{
    public class InventoryProfile:Profile
    {
        public InventoryProfile() 
        {
            CreateMap<GetProductInventoryResponse, Inventory>()
                .ReverseMap();

            CreateMap<InsertProductInventoryRequest, Inventory>()
                .ReverseMap();
        }
    }
}
