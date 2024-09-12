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
    public class UserStatusService : IUserStatusService
    {
        private readonly IUserStatusRepository _userStatusRepository;
        private readonly IMapper _mapper;
        public UserStatusService(IUserStatusRepository userStatusRepository, IMapper mapper)
        {
            _userStatusRepository = userStatusRepository;
            _mapper = mapper;
        }
        public async Task<UserStatusDescriptionResponse> GetUserStatusDescription(int userStatusId)
        {
            UserStatus userStatus = await _userStatusRepository.GetUserStatusDescription(userStatusId);
            UserStatusDescriptionResponse userStatusDescription = _mapper.Map<UserStatusDescriptionResponse>(userStatus);
            return userStatusDescription;
        }
    }
}
