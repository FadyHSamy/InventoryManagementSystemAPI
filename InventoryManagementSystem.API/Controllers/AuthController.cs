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

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            try
            {
               var res =  await _authService.Login(loginRequestDto);
                ApiResponse<object> response = ApiResponseHelper.Success<object>(Request, "User Logged successfully",res);
                return Ok(response);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
