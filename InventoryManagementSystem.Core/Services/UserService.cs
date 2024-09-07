using AutoMapper;
using BCrypt.Net;
using InventoryManagementSystem.Core.DTOs.User;
using InventoryManagementSystem.Core.Entities.Shared;
using InventoryManagementSystem.Core.Entities.User;
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

        public async Task AddUserAsync(AddingUserDto addUserDto)
        {
            var user = _mapper.Map<User>(addUserDto);
            // Hash the password and other logic here
            user.PasswordHash = HashPassword(addUserDto.PlainPassword);
            await _userRepository.AddAsync(user);
        }

        public string HashPassword(string plainPassword)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(plainPassword);
            return passwordHash;
        }
    }
}