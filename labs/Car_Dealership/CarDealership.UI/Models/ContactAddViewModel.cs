using CarDealership.Models.Tables;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CarDealership.UI.Models
{
    public class ContactAddViewModel
    {
        [Required]
        public string Name     { get; set; }
        [Required]
        public string Message  { get; set; }

        public string Email    { get; set; }
        public string Phone    { get; set; }
        public Contact Contact { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            var namePattern = new Regex(@"^[A-Za-z\'\- ]+$");
            var emailPattern = new Regex(@"^.+@[^\.].*\.[a-z]{2,}$");
            var phonePattern = new Regex(
                @"^\d?[\-)(\.]{0,2}(\d{3})[\-)(.]?[\-)(.]?(\d{3})[\-)(.]?(\d{4})$");

            if (Name.Length > 50)
            {
                errors.Add(new ValidationResult("Name must be less than 50 characters."));
            }

            if (string.IsNullOrEmpty(Contact.Phone) && string.IsNullOrEmpty(Email))
            {
                errors.Add(new ValidationResult("Customer phone or email must be provided."));
            }

            if (Phone.Length > 11 || !phonePattern.IsMatch(Phone))
            {
                errors.Add(new ValidationResult("Phone must be a valid US number."));
            }

            if (Email.Length > 50 || !emailPattern.IsMatch(Email))
            {
                errors.Add(new ValidationResult("Email address must be less than 50 characters."));
            }

            if (Message.Length > 500)
            {
                errors.Add(new ValidationResult("Message must be less than 500 characters."));
            }

            return errors;
        }
    }
}