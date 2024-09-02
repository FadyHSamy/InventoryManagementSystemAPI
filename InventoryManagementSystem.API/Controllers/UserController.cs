﻿using InventoryManagementSystem.Core.DTOs.User;
using InventoryManagementSystem.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("add")]
        public async Task<IActionResult> AddUser([FromBody] AddingUserDto addUserDto)
        {
            var result = await _userService.AddUserAsync(addUserDto);
            if (result.Success)
            {
                return Ok();
            }
            return BadRequest(result.Message);
        }
    }
}