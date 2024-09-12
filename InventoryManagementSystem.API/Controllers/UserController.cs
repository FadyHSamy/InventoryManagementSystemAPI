using InventoryManagementSystem.API.Middlewares;
using InventoryManagementSystem.Core.DTOs.UserDto;
using InventoryManagementSystem.Core.Entities.Shared;
using InventoryManagementSystem.Core.Interfaces.Services.AllUserIServices;
using InventoryManagementSystem.Core.Utilities.Helpers;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InventoryManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] AddingUserRequest addUserDto)
        {
            try
            {
                await _userService.AddUser(addUserDto);
                ApiResponse<object> response = ApiResponseHelper.Success<object>(Request, "User added successfully");
                return Ok(response);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("GetUserInformation")]
        public async Task<IActionResult> GetUserInformation(string Username)
        {
            try
            {
                UserInformationResponse GetUserInformationDto = await _userService.GetUserInformation(Username);
                ApiResponse<object> response = ApiResponseHelper.Success<object>(Request, "User information successfully fetched from the database", new { userInformation = GetUserInformationDto });
                return Ok(response);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}