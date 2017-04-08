using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CarDealership.UI.Models.CustomValidation
{
    public class ValidPasswordAttribute : ValidationAttribute
    {
        public override bool IsValid(object input)
        {
            if (input is string)
            {
                string password = input.ToString();
                return (password.Length >= 5 && password.Length <= 50) &&
                        password.Any(c => char.IsUpper(c)) &&
                        password.Any(c => char.IsLower(c)) &&
                        password.Any(c => char.IsDigit(c));
            }
            else return false;
        }
    }
}