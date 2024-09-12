using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static InventoryManagementSystem.Core.Validators.UserValidations;

namespace InventoryManagementSystem.Core.DTOs.UserDto
{
    public class AddingUserRequest
    {
        [UsernameValidation]
        public string Username { get; set; }
        [PasswordValidation]
        public string PlainPassword { get; set; }
        [MobileNumberValidation(required: false)]
        public string? MobileNumber { get; set; }
        [EmailValidation(required: false)]
        public string? Email { get; set; }
    }
}
