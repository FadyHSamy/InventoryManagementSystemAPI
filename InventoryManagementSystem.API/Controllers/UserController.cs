using InventoryManagementSystem.API.Middlewares;
using InventoryManagementSystem.Core.DTOs.User;
using InventoryManagementSystem.Core.Entities.Shared;
using InventoryManagementSystem.Core.Interfaces.Services;
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
            _userService = userService;;
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] AddingUserDto addUserDto)
        {
            try
            {
                await _userService.AddUser(addUserDto);
                var data = new { Name = "Test", Value = 123 };
                var response = ApiResponseHelper.Success<object>(Request, "Request was successful");
                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpGet("GetUserInformation")]
        public async Task<IActionResult> GetUserInformation(string Username)
        {
            try
            {
                GetUserInformationDto GetUserInformationDto = await _userService.GetUserInformation(Username);
                return Ok(GetUserInformationDto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}