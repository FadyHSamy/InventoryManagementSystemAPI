﻿using Azure;
using InventoryManagementSystem.Core.DTOs.AuthDto;
using InventoryManagementSystem.Core.Entities.User;
using InventoryManagementSystem.Core.Exceptions;
using InventoryManagementSystem.Core.Interfaces.Repositories.AllAuthRepository;
using InventoryManagementSystem.Core.Interfaces.Services.AllAuthServices;
using InventoryManagementSystem.Core.Interfaces.Services.AllJwtServices;
using InventoryManagementSystem.Core.Interfaces.Services.AllUserIServices;
using InventoryManagementSystem.Core.Services.AllUserServices;
using InventoryManagementSystem.Core.Utilities.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IServiceProvider _serviceProvider;
        public AuthService(IAuthRepository authRepository, IServiceProvider serviceProvider)
        {
            _authRepository = authRepository;
            _serviceProvider = serviceProvider;
        }
        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            string passwordHashed = await _serviceProvider.GetRequiredService<IUserService>().GetUserHashPassword(loginRequestDto.Username);

            bool isValid = Helpers.VerifyHashPassword(loginRequestDto.Password, passwordHashed);
            if (!isValid)
            {
                throw new AuthException("username or password is incorrect");
            }
            string token = await _serviceProvider.GetRequiredService<IJwtService>().GenerateUserToken(loginRequestDto.Username);

            var userInformation = await _serviceProvider.GetRequiredService<IUserService>().GetUserInformation(loginRequestDto.Username);


            LoginResponseDto response = new LoginResponseDto()
            {
                token = token,
                user = new UserLoginResponse
                {
                    username = loginRequestDto.Username,
                    role = userInformation.RoleName
                }
            };

            return response;
        }

        public async Task<string> RefreshToken(string token)
        {
            var principal = _serviceProvider.GetRequiredService<IJwtService>().ValidateToken(token);
            if (principal == null)
            {
                return null; // Invalid token
            }

            var username = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return await _serviceProvider.GetRequiredService<IJwtService>().GenerateUserToken(username);
        }

    }
}
