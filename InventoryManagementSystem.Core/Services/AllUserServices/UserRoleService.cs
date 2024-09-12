using AutoMapper;
using InventoryManagementSystem.Core.DTOs.UserDto;
using InventoryManagementSystem.Core.Entities.User;
using InventoryManagementSystem.Core.Interfaces.Repositories.AllUserIRepository;
using InventoryManagementSystem.Core.Interfaces.Services.AllUserIServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.Services.AllUserServices
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRolesRepository _userRolesRepository;
        private readonly IMapper _mapper;
        public UserRoleService(IUserRolesRepository userRolesRepository, IMapper mapper)
        {
            _userRolesRepository = userRolesRepository;
            _mapper = mapper;
        }
        public async Task<UserRoleDescriptionResponse> GetUserRoleDescription(int userRoleId)
        {
            UserRoles userRoles = await _userRolesRepository.GetUserRoleDescription(userRoleId);
            UserRoleDescriptionResponse roleDescription = _mapper.Map<UserRoleDescriptionResponse>(userRoles);
            return roleDescription;
        }
    }
}
