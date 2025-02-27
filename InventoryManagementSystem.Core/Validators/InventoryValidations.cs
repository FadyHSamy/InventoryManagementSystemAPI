﻿using InventoryManagementSystem.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Core.Validators
{
    public class InventoryValidations
    {
        public class StockQuantityValidation : ValidationAttribute
        {
            private readonly bool _required;

            public StockQuantityValidation(bool required = true)
            {
                _required = required;
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value is int stockQuantity)
                {
                    // Check if the value is valid
                    if (!_required && stockQuantity < 0)
                    {
                        return ValidationResult.Success;
                    }

                    if (stockQuantity < 0)
                    {
                        return new ValidationResult("Stock Quantity cannot be negative.");
                    }

                    return ValidationResult.Success;
                }

                return new ValidationResult("Invalid Stock Quantity.");
            }

        }
    }
}
