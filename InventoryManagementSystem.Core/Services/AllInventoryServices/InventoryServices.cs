using AutoMapper;
using InventoryManagementSystem.Core.DTOs.InventoryDto;
using InventoryManagementSystem.Core.DTOs.ProductDto;
using InventoryManagementSystem.Core.Entities.Inventory;
using InventoryManagementSystem.Core.Exceptions;
using InventoryManagementSystem.Core.Interfaces.Repositories.AllInventoryIRepository;
using InventoryManagementSystem.Core.Interfaces.Services.AllCategoryIServices;
using InventoryManagementSystem.Core.Interfaces.Services.AllInventoryIServices;
using InventoryManagementSystem.Core.Interfaces.Services.AllProductIServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.Services.AllInventoryServices
{
    public class InventoryServices : IInventoryServices
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IMapper _mapper;
        private readonly IServiceProvider _serviceProvider;
        public InventoryServices(IInventoryRepository inventoryRepository, IMapper mapper, IProductService productService, IServiceProvider serviceProvider)
        {
            _inventoryRepository = inventoryRepository;
            _mapper = mapper;
            _serviceProvider = serviceProvider;
        }

        public async Task<GetProductInventoryResponse> GetProductInventory(int productId)
        {
            try
            {
                Inventory inventory = await _inventoryRepository.GetProductInventory(productId);

                if (inventory == null)
                {
                    throw new NotFoundException("There Is No Inventory");
                }

                GetProductInventoryResponse response = _mapper.Map<GetProductInventoryResponse>(inventory);
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task InsertProductInventory(InsertProductInventoryRequest insertProductInventoryRequest)
        {
            try
            {
                Inventory inventory = _mapper.Map<Inventory>(insertProductInventoryRequest);
                GetProductResponse productResponse = await _serviceProvider.GetRequiredService<IProductService>().GetProduct(inventory.ProductId);
                if (productResponse == null)
                {
                    throw new NotFoundException("There Is No Product With This Id");
                }
                await _inventoryRepository.InsertProductInventory(inventory);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteProductInventory(int productId)
        {
            try
            {
                await _inventoryRepository.DeleteProductInventory(productId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public async Task AdjustProductInventory(int productId , int stockAdjustment)
        {
            try
            {
                await _inventoryRepository.AdjustProductInventory(productId, stockAdjustment);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
