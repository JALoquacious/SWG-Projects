using CarDealership.Models.Queries;
using CarDealership.Models.Tables;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace CarDealership.UI.Models
{
    public class ModelAddViewModel : IValidatableObject
    {
        public string NewModelName                           { get; set; }
        public Make Make                                     { get; set; }
        public IEnumerable<SelectListItem> Makes             { get; set; }
        public IEnumerable<ModelUserQueryRow> ModelUserTable { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            var modelNameConstraint = new Regex(@"^[A-Za-z0-9 \-\']+$");

            if (string.IsNullOrEmpty(NewModelName) || NewModelName.Length > 25)
            {
                errors.Add(new ValidationResult("Model name must be 1-25 characters.", new[] { "NewModelName" }));
            }

            if (!string.IsNullOrEmpty(NewModelName) && !modelNameConstraint.IsMatch(NewModelName))
            {
                errors.Add(new ValidationResult("Model name allows letters, numbers, hyphens, and apostrophes.", new[] { "NewModelName" }));
            }

            return errors;
        }
    }
}