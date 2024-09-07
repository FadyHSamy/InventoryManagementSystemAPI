using InventoryManagementSystem.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.Validators
{
    public class UserValidations
    {
        public class UsernameValidation : ValidationAttribute
        {
            private readonly bool _required;

            public UsernameValidation(bool required = true)
            {
                _required = required;
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var username = value as string;

                if (!_required && string.IsNullOrWhiteSpace(username))
                {
                    return ValidationResult.Success;
                }

                if (string.IsNullOrWhiteSpace(username))
                {
                    throw new ValidationCustomException("Username is required.");
                }

                if (username.Length < 3 || username.Length > 20)
                {
                    throw new ValidationCustomException("Username must be between 3 and 20 characters.");
                }

                return ValidationResult.Success;
            }

        }

        public class PasswordValidation : ValidationAttribute
        {
            private readonly bool _required;
            public PasswordValidation(bool required = true)
            {
                _required = required;
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var password = value as string;

                // If not required and no value provided, it's valid.
                if (!_required && string.IsNullOrWhiteSpace(password))
                {
                    return ValidationResult.Success;
                }

                // If required but no value provided, it's invalid.
                if (string.IsNullOrWhiteSpace(password))
                {
                    throw new ValidationCustomException("Password is required.");
                }

                if (password.Length < 8)
                {
                    throw new ValidationCustomException("Password must be at least 8 characters long.");
                }

                return ValidationResult.Success;
            }
        }

        public class MobileNumberValidation : ValidationAttribute
        {
            private readonly bool _required;
            private readonly Regex _egyptRegex = new Regex(@"^01[0125][0-9]{8}$", RegexOptions.Compiled);

            public MobileNumberValidation(bool required = true)
            {
                _required = required;
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var mobileNumber = value as string;

                // If not required and no value provided, it's valid.
                if (!_required && string.IsNullOrWhiteSpace(mobileNumber))
                {
                    return ValidationResult.Success;
                }

                // If required but no value provided, it's invalid.
                if (string.IsNullOrWhiteSpace(mobileNumber))
                {
                    throw new ValidationCustomException("Mobile number is required.");
                }

                // Validate against the regex for Egyptian mobile numbers.
                if (!_egyptRegex.IsMatch(mobileNumber))
                {
                    throw new ValidationCustomException("Invalid mobile number format.");
                }

                return ValidationResult.Success;
            }
        }

        public class EmailValidation : ValidationAttribute
        {
            private readonly bool _required;

            public EmailValidation(bool required = true)
            {
                _required = required;
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var email = value as string;

                if (string.IsNullOrWhiteSpace(email))
                {
                    throw new ValidationCustomException("Email is required.");
                }

                var emailAttribute = new EmailAddressAttribute();
                if (!emailAttribute.IsValid(email))
                {
                    throw new ValidationCustomException("Invalid email address.");
                }

                return ValidationResult.Success;
            }

        }
    }
}
