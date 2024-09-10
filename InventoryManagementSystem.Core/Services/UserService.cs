using AutoMapper;
using BCrypt.Net;
using InventoryManagementSystem.Core.DTOs.User;
using InventoryManagementSystem.Core.Entities.Shared;
using InventoryManagementSystem.Core.Entities.User;
using InventoryManagementSystem.Core.Exceptions;
using InventoryManagementSystem.Core.Interfaces.Repositories;
using InventoryManagementSystem.Core.Interfaces.Services;

namespace InventoryManagementSystem.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task AddUser(AddingUserDto addUserDto)
        {
            try
            {
                var user = _mapper.Map<User>(addUserDto);
                user.PasswordHash = HashPassword(addUserDto.PlainPassword);
                await _userRepository.AddUser(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string HashPassword(string plainPassword)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(plainPassword);
            return passwordHash;
        }
        public async Task<GetUserInformationDto> GetUserInformation(string Username)
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
            GetUserInformationDto GetUserInformationDto = _mapper.Map<GetUserInformationDto>(user);
            return GetUserInformationDto;
        }
    }
}