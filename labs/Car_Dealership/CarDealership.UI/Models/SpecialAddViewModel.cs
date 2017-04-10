using CarDealership.Models.Tables;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarDealership.UI.Models
{
    public class SpecialAddViewModel
    {
        [Required]
        public Special NewSpecial { get; set; }

        [Required]
        public IEnumerable<Special> SpecialsList { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (NewSpecial.Name.Length > 75)
            {
                errors.Add(new ValidationResult("Title cannot be longer than 75 characters."));
            }

            if (NewSpecial.Description.Length > 500)
            {
                errors.Add(new ValidationResult("Description cannot be longer than 500 characters."));
            }

            return errors;
        }
    }
}