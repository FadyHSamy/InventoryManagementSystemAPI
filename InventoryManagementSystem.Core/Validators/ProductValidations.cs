using InventoryManagementSystem.Core.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace InventoryManagementSystem.Core.Validators
{
    public class ProductValidations
    {

        public class ProductNameValidation : ValidationAttribute
        {
            private readonly bool _required;

            public ProductNameValidation(bool required = true)
            {
                _required = required;
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var name = value as string;

                if (!_required && string.IsNullOrWhiteSpace(name))
                {
                    return ValidationResult.Success;
                }

                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new ValidationCustomException("Product name is required.");
                }

                const string regexPattern = @"^[a-zA-Z\u0600-\u06FF\s]*[a-zA-Z\u0600-\u06FF]{5,}[a-zA-Z\u0600-\u06FF\s]*$";

                var regex = new Regex(regexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);


                if (!regex.IsMatch(name))
                {
                    throw new ValidationCustomException("Product name must contain at least 5 characters.");
                }


                return ValidationResult.Success;
            }

        }

        public class ProductDescriptionValidation : ValidationAttribute
        {
            private readonly bool _required;

            public ProductDescriptionValidation(bool required = false)
            {
                _required = required;
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var description = value as string;

                if (!_required && string.IsNullOrWhiteSpace(description))
                {
                    return ValidationResult.Success;
                }

                if (string.IsNullOrWhiteSpace(description))
                {
                    throw new ValidationCustomException("Product description is required.");
                }

                return ValidationResult.Success;
            }

        }

        public class ProductPriceValidation : ValidationAttribute
        {
            private readonly bool _required;

            public ProductPriceValidation(bool required = true)
            {
                _required = required;
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value is decimal productPrice)
                {
                    // Check if the value is valid
                    if (!_required && productPrice < 0)
                    {
                        return ValidationResult.Success;
                    }

                    if (productPrice <= 0)
                    {
                        return new ValidationResult("Product Price must be greater than zero.");
                    }

                    return ValidationResult.Success;
                }

                return new ValidationResult("Invalid Product Price.");
            }

        }

        public class ProductIdValidation : ValidationAttribute
        {
            private readonly bool _required;

            public ProductIdValidation(bool required = true)
            {
                _required = required;
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value is int productId)
                {
                    // Check if the value is valid
                    if (!_required && productId == 0)
                    {
                        return ValidationResult.Success;
                    }

                    if (productId <= 0)
                    {
                        return new ValidationResult("Product Id must be greater than zero.");
                    }

                    return ValidationResult.Success;
                }

                return new ValidationResult("Invalid Product Id.");
            }

        }

    }
}
