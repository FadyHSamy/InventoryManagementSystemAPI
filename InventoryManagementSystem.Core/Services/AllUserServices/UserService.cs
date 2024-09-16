using AutoMapper;
using BCrypt.Net;
using InventoryManagementSystem.Core.DTOs.UserDto;
using InventoryManagementSystem.Core.Entities.Shared;
using InventoryManagementSystem.Core.Entities.User;
using InventoryManagementSystem.Core.Exceptions;
using InventoryManagementSystem.Core.Interfaces.Repositories.AllUserIRepository;
using InventoryManagementSystem.Core.Interfaces.Services.AllUserIServices;
using InventoryManagementSystem.Core.Utilities.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagementSystem.Core.Services.AllUserServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IServiceProvider _serviceProvider;

        public UserService(IUserRepository userRepository, IMapper mapper, IServiceProvider serviceProvider)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _serviceProvider = serviceProvider;
        }

        public async Task AddUser(AddingUserRequest addUserDto)
        {
            try
            {
                var user = _mapper.Map<User>(addUserDto);
                user.PasswordHash = Helpers.HashPassword(addUserDto.PlainPassword);
                await _userRepository.AddUser(user);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<UserInformationResponse> GetUserInformation(string Username)
        {
            try
            {
                if (Username == null)
                {
                    throw new ValidationCustomException("Username is required");
                }

                User user = await _userRepository.GetUserInformation(Username);
                if (user == null)
                {
                    throw new NotFoundException("Username not found");
                }

                UserRoleDescriptionResponse userRoleDescription = await _serviceProvider.GetRequiredService<IUserRoleService>().GetUserRoleDescription(user.RoleId);
                UserStatusDescriptionResponse userStatusDescriptionResponse = await _serviceProvider.GetRequiredService<IUserStatusService>().GetUserStatusDescription(user.StatusId);

                UserInformationResponse GetUserInformationDto = _mapper.Map<UserInformationResponse>(user);
                GetUserInformationDto.RoleName = userRoleDescription.RoleName;
                GetUserInformationDto.StatusDescripton = userStatusDescriptionResponse.StatusDescripton;
                return GetUserInformationDto;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}