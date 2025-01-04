using InventoryManagementSystem.Core.DTOs.AuthDto;
using InventoryManagementSystem.Core.Entities.Shared;
using InventoryManagementSystem.Core.Exceptions;
using InventoryManagementSystem.Core.Interfaces.Services.AllAuthServices;
using InventoryManagementSystem.Core.Utilities.Helpers;
using InventoryManagementSystem.Core.Validators;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            try
            {
                var result = await _authService.Login(loginRequestDto);
                return Ok(ApiResponseHelper.Success(Request, "User logged in successfully", result));
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost("RefreshToken")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto request)
        {
            try
            {
                var result = await _authService.RefreshToken(request.Token, request.RefreshToken);
                return Ok(ApiResponseHelper.Success(Request, "Tokens refreshed successfully", result));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
