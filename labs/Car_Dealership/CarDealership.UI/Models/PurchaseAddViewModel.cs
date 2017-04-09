using CarDealership.Models.Queries;
using CarDealership.Models.Tables;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace CarDealership.UI.Models
{
    public class PurchaseAddViewModel : IValidatableObject
    {
        public Customer Customer                        { get; set; }
        public PaymentType PaymentType                  { get; set; }
        public Sale Sale                                { get; set; }
        public State State                              { get; set; }
        public VehicleDetail VehicleDetail              { get; set; }
        public IEnumerable<SelectListItem> States       { get; set; }
        public IEnumerable<SelectListItem> PaymentTypes { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors        = new List<ValidationResult>();
            var zipPattern    = new Regex(@"^\d{5}$");
            var cityPattern   = new Regex(@"^[A-Za-z\'\- ]+$");
            var streetPattern = new Regex(@"^[0-9A-Za-z\-\.\,\# ]+$");
            var emailPattern  = new Regex(@"^.+@[^\.].*\.[a-z]{2,}$");
            var phonePattern  = new Regex(
                @"^\d?[\-)(\.]{0,2}(\d{3})[\-)(.]?[\-)(.]?(\d{3})[\-)(.]?(\d{4})$");

            if (Customer.Name.Length > 50)
            {
                errors.Add(new ValidationResult("Name must be less than 50 characters."));
            }

            if (string.IsNullOrEmpty(Customer.Phone) && string.IsNullOrEmpty(Customer.Email))
            {
                errors.Add(new ValidationResult("Customer phone or email must be provided."));
            }

            if (Customer.Phone.Length > 11 || !phonePattern.IsMatch(Customer.Phone))
            {
                errors.Add(new ValidationResult("Phone must be a valid US number."));
            }

            if (Customer.Email.Length > 50 || !emailPattern.IsMatch(Customer.Email))
            {
                errors.Add(new ValidationResult("Email address must be less than 50 characters."));
            }

            if (Customer.Street1.Length > 50 || !streetPattern.IsMatch(Customer.Street1) ||
                Customer.Street2.Length > 50 || !streetPattern.IsMatch(Customer.Street2))
            {
                errors.Add(new ValidationResult("Each street address line must be less than 50 characters and a valid address."));
            }

            if (Customer.City.Length > 50)
            {
                errors.Add(new ValidationResult("City name must be less than 50 characters."));
            }

            if (Customer.City.Length > 5 || !zipPattern.IsMatch(Customer.Zip))
            {
                errors.Add(new ValidationResult("City name must be less than 50 characters."));
            }

            if (Sale.SalePrice < 0 || Sale.SalePrice > 999999m)
            {
                errors.Add(new ValidationResult("Sale price must be between 0 and $999,999."));
            }

            return errors;
        }
    }
}