using CarDealership.Models.Queries;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarDealership.UI.Models
{
    public class MakeAddViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "An automaker's name is required.")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[A-Za-z \-\']+$")]
        public string NewMakeName { get; set; }

        public IEnumerable<MakeUserQueryRow> MakeUserTable { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (string.IsNullOrEmpty(NewMakeName) || NewMakeName.Length < 2 || NewMakeName.Length > 25)
            {
                errors.Add(new ValidationResult("Make name must be between 2-25 characters.", new[] { "NewMakeName" }));
            }

            return errors;
        }
    }
}