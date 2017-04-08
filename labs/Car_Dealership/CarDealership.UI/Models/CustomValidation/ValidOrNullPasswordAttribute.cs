using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CarDealership.UI.Models.CustomValidation
{
    public class ValidOrNullPasswordAttribute : ValidationAttribute
    {
        public override bool IsValid(object input)
        {
            string password = input as string;
            return 
                String.IsNullOrEmpty(password) ||
                password.Length >= 5 && password.Length <= 50 &&
                password.Any(c => char.IsUpper(c)) &&
                password.Any(c => char.IsLower(c)) &&
                password.Any(c => char.IsDigit(c));
        }
    }
}