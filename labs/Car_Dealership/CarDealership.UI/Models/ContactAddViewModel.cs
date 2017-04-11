using CarDealership.Models.Tables;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CarDealership.UI.Models
{
    public class ContactAddViewModel : IValidatableObject
    {
        public Contact Contact { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            var namePattern = new Regex(@"^[A-Za-z\'\- ]+$");
            var emailPattern = new Regex(@"^.+@[^\.].*\.[a-z]{2,}$");
            var phonePattern = new Regex(
                @"^\d?[\-)(\.]{0,2}(\d{3})[\-)(.]?[\-)(.]?(\d{3})[\-)(.]?(\d{4})$");

            if (string.IsNullOrEmpty(Contact.Name) || Contact.Name.Length < 2 || Contact.Name.Length > 50 || !namePattern.IsMatch(Contact.Name))
            {
                errors.Add(new ValidationResult("Name must be between 2-50 characters.", new[] { "Contact.Name" }));
            }

            if (string.IsNullOrEmpty(Contact.Phone) && string.IsNullOrEmpty(Contact.Email))
            {
                errors.Add(new ValidationResult("Customer phone or email must be provided.", new[] { "Contact.Phone", "Contact.Email" }));
            }

            if (!string.IsNullOrEmpty(Contact.Phone) && (Contact.Phone.Length > 11 || !phonePattern.IsMatch(Contact.Phone)))
            {
                errors.Add(new ValidationResult("Phone must be a valid US number.", new[] { "Contact.Phone" }));
            }

            if (!string.IsNullOrEmpty(Contact.Email) && (Contact.Email.Length > 50 || !emailPattern.IsMatch(Contact.Email)))
            {
                errors.Add(new ValidationResult("Email address must be valid and less than 50 characters.", new[] { "Contact.Email" }));
            }

            if (string.IsNullOrEmpty(Contact.Message) || Contact.Message.Length > 500)
            {
                errors.Add(new ValidationResult("Message must be less than 500 characters.", new[] { "Contact.Message" }));
            }

            return errors;
        }
    }
}