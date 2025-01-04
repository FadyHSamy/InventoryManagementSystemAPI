using Azure;
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
        private readonly IServiceProvider _serviceProvider;
        public AuthService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            string passwordHashed = await _serviceProvider.GetRequiredService<IUserService>().GetUserHashPassword(loginRequestDto.Username);

            bool isValid = Helpers.VerifyHashPassword(loginRequestDto.Password, passwordHashed);
            if (!isValid)
            {
                throw new AuthException("Username or password is incorrect");
            }

            var jwtService = _serviceProvider.GetRequiredService<IJwtService>();
            string accessToken = await jwtService.GenerateUserToken(loginRequestDto.Username);
            string refreshToken = jwtService.GenerateRefreshToken(loginRequestDto.Username);

            var userInformation = await _serviceProvider.GetRequiredService<IUserService>().GetUserInformation(loginRequestDto.Username);
            return new LoginResponseDto
            {
                Token = accessToken,
                RefreshToken = refreshToken,
                User = new UserLoginResponse
                {
                    username = loginRequestDto.Username,
                    role = userInformation.RoleName
                }
            };
        }

        public async Task<TokenResponseDto> RefreshToken(string expiredToken, string refreshToken)
        {
            var jwtService = _serviceProvider.GetRequiredService<IJwtService>();

            var principal = jwtService.ValidateRefreshToken(refreshToken);
            if (principal == null)
            {
                throw new AuthException("Invalid refresh token");
            }

            var username = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(username))
            {
                throw new AuthException("Invalid token data");
            }

            string newAccessToken = await jwtService.GenerateUserToken(username);
            string newRefreshToken = jwtService.GenerateRefreshToken(username);

            return new TokenResponseDto
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }

    }
}
