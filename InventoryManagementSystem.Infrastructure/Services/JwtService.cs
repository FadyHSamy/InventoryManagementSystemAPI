using InventoryManagementSystem.Core.DTOs.AuthDto;
using InventoryManagementSystem.Core.DTOs.UserDto;
using InventoryManagementSystem.Core.Entities.User;
using InventoryManagementSystem.Core.Exceptions;
using InventoryManagementSystem.Core.Interfaces.Services.AllJwtServices;
using InventoryManagementSystem.Core.Interfaces.Services.AllUserIServices;
using InventoryManagementSystem.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtOptions _jwtOptions;
        private readonly IUserService _userService;
        public JwtService(IOptions<JwtOptions> jwtOptions, IUserService userService)
        {
            _jwtOptions = jwtOptions.Value;
            _userService = userService;
        }
        public async Task<string> GenerateUserToken(string username)
        {
            var userInformation = await _userService.GetUserInformation(username);
            return GenerateToken(userInformation, _jwtOptions.ExpiryMinutes);
        }
        public string GenerateRefreshToken(string username)
        {
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                        new Claim("username", username),
                    }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public ClaimsPrincipal ValidateRefreshToken(string refreshToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

            try
            {
                var principal = tokenHandler.ValidateToken(refreshToken, new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateLifetime = true
                }, out var validatedToken);

                return principal;
            }
            catch
            {
                return null;
            }
        }

        private string GenerateToken(UserInformationResponse userInformationResponse, int expiryMinutes)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("username", userInformationResponse.Username),
            }),
                Expires = DateTime.UtcNow.AddMinutes(expiryMinutes),
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
