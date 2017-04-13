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
        public State State                              { get; set; }
        public Sale Sale                                { get; set; }
        public VehicleDetail VehicleDetail              { get; set; }
        public IEnumerable<SelectListItem> States       { get; set; }
        public IEnumerable<SelectListItem> PaymentTypes { get; set; }

        public PurchaseAddViewModel()
        {
            Customer = new Customer();
            PaymentTypes = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text  = "Bank Finance",
                    Value = "1"
                },
                new SelectListItem()
                {
                    Text  = "Cash",
                    Value = "2"
                },
                new SelectListItem()
                {
                    Text  = "Dealer Finance",
                    Value = "3"
                }
            };
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors        = new List<ValidationResult>();
            var pricePattern  = new Regex(@"^\d{3,6}$");
            var namePattern   = new Regex(@"^[A-Za-z\'\- ]+$");
            var cityPattern   = new Regex(@"^[A-Za-z\'\- ]+$");
            var streetPattern = new Regex(@"^[0-9A-Za-z\-\.\,\# ]+$");
            var zipPattern    = new Regex(@"^\d{5}$");
            var emailPattern  = new Regex(@"^.+@[^\.].*\.[a-z]{2,}$");
            var phonePattern  = new Regex(
                @"^\d?[\-)(\.]{0,2}(\d{3})[\-)(.]?[\-)(.]?(\d{3})[\-)(.]?(\d{4})$");

            if (string.IsNullOrEmpty(Customer.Name) || Customer.Name.Length > 50 || !namePattern.IsMatch(Customer.Name))
            {
                errors.Add(new ValidationResult("Name must be 1-50 characters.", new[] { "Customer.Name" }));
            }

            if (string.IsNullOrEmpty(Customer.Phone) && string.IsNullOrEmpty(Customer.Email))
            {
                errors.Add(new ValidationResult("Customer phone or email must be provided.", new[] { "Customer.Phone", "Customer.Email" }));
            }

            if (!string.IsNullOrEmpty(Customer.Phone) && (Customer.Phone.Length > 11 || !phonePattern.IsMatch(Customer.Phone)))
            {
                errors.Add(new ValidationResult("Phone must be a valid US number.", new[] { "Customer.Phone" }));
            }

            if (!string.IsNullOrEmpty(Customer.Email) && (Customer.Email.Length > 50 || !emailPattern.IsMatch(Customer.Email)))
            {
                errors.Add(new ValidationResult("Email address must be less than 50 characters.", new[] { "Customer.Email" }));
            }

            if (string.IsNullOrEmpty(Customer.Street1) || Customer.Street1.Length > 50 || !streetPattern.IsMatch(Customer.Street1))
            {
                errors.Add(new ValidationResult("Street address 1 must be a valid address and less than 50 characters.", new[] { "Customer.Street1" }));
            }

            if (string.IsNullOrEmpty(Customer.City) || Customer.City.Length > 50)
            {
                errors.Add(new ValidationResult("City name must be less than 50 characters.", new[] { "Customer.City" }));
            }

            if (string.IsNullOrEmpty(Customer.Zip) || Customer.Zip.Length > 5 || !zipPattern.IsMatch(Customer.Zip))
            {
                errors.Add(new ValidationResult("Zip code must be 5 characters.", new[] { "Customer.Zip" }));
            }

            if (string.IsNullOrEmpty(Sale.PurchasePrice.ToString()) || !pricePattern.IsMatch(Sale.PurchasePrice.ToString()))
            {
                errors.Add(new ValidationResult("Sale price must be between 0 and $999,999.", new[] { "Sale.PurchasePrice" }));
            }

            if (Sale.PurchasePrice > VehicleDetail.MSRP)
            {
                errors.Add(new ValidationResult("Sale price cannot exceed vehicle MSRP.", new[] { "Sale.PurchasePrice" }));
            }

            if (Sale.PurchasePrice < .95m * VehicleDetail.SalePrice)
            {
                errors.Add(new ValidationResult("Purchase price cannot be less than 95% of original salesfloor price.", new[] { "Sale.PurchasePrice" }));
            }

            return errors;
        }
    }
}