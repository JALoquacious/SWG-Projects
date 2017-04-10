using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace CarDealership.UI.Models
{
    public class VehicleAdminViewModel : IValidatableObject
    {
        public int BodyStyleId                            { get; set; }
        public int ExteriorColorId                        { get; set; }
        public int InteriorColorId                        { get; set; }
        public int MakeId                                 { get; set; }
        public int ModelId                                { get; set; }
        public int Year                                   { get; set; }
        public bool IsAutomatic                           { get; set; }
        public bool IsUsed                                { get; set; }
        public bool IsFeatured                            { get; set; }
        public string UserId                              { get; set; }
        public string VIN                                 { get; set; }
        public string Description                         { get; set; }
        public string Image                               { get; set; }
        public decimal SalePrice                          { get; set; }
        public decimal MSRP                               { get; set; }
        public decimal Mileage                            { get; set; }
        public Vehicle Vehicle                            { get; set; }
        public HttpPostedFileBase ImageUpload             { get; set; }
        public IEnumerable<SelectListItem> BodyStyle      { get; set; }
        public IEnumerable<SelectListItem> Condition      { get; set; }
        public IEnumerable<SelectListItem> ExteriorColor  { get; set; }
        public IEnumerable<SelectListItem> InteriorColor  { get; set; }
        public IEnumerable<SelectListItem> Make           { get; set; }
        public IEnumerable<SelectListItem> Model          { get; set; }
        public IEnumerable<SelectListItem> Transmission   { get; set; }

        public VehicleAdminViewModel()
        {
            BodyStyle = new List<SelectListItem>()
            {
                new SelectListItem() { Value = "1", Text = "Car"   },
                new SelectListItem() { Value = "2", Text = "SUV"   },
                new SelectListItem() { Value = "3", Text = "Truck" },
                new SelectListItem() { Value = "4", Text = "Van"   }
            };

            Condition = new List<SelectListItem>()
            {
                new SelectListItem() { Value = "false", Text = "New"  },
                new SelectListItem() { Value = "true",  Text = "Used" }
            };

            Transmission = new List<SelectListItem>()
            {
                new SelectListItem() { Value = "false", Text = "Manual"    },
                new SelectListItem() { Value = "true",  Text = "Automatic" }
            };
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors                 = new List<ValidationResult>();
            var decimalConstraint      = new Regex(@"^[0-9]+(\.[0-9]+)?$");
            var integerConstraint      = new Regex(@"^[0-9]+$");
            var alphaNumericConstraint = new Regex(@"^(\w|\d)+$");

            if (!integerConstraint.IsMatch(Year.ToString()) || Year < 2000 || Year > DateTime.Today.Year + 1)
            {
                errors.Add(new ValidationResult("Year must be between 2000 and next year."));
            }

            if (!decimalConstraint.IsMatch(Mileage.ToString()) || Mileage < 0m || Mileage > 1000000m)
            {
                errors.Add(new ValidationResult("Mileage must be between 0 and 1,000,000."));
            }

            if (!decimalConstraint.IsMatch(Mileage.ToString()) || MSRP <= 0m || MSRP >= 1000000m)
            {
                errors.Add(new ValidationResult("MSRP must be between 0 and $1,000,000."));
            }

            if (!decimalConstraint.IsMatch(Mileage.ToString()) || SalePrice <= 0m || SalePrice >= 1000000m)
            {
                errors.Add(new ValidationResult("Sale price must be between 0 and $1,000,000."));
            }

            if (!alphaNumericConstraint.IsMatch(VIN) || VIN.Length != 17)
            {
                errors.Add(new ValidationResult("VIN must be 17 digits in length."));
            }

            if (ImageUpload != null && ImageUpload.ContentLength > 0)
            {
                var extensions = new string[] { ".jpg", ".png", ".gif", ".jpeg" };

                var extension = Path.GetExtension(ImageUpload.FileName);

                if (!extensions.Contains(extension))
                {
                    errors.Add(new ValidationResult("Image file must be a jpg, png, gif, or jpeg."));
                }
            } // image is NULLABLE
            //else
            //{
            //    errors.Add(new ValidationResult("Image file is required."));
            //}

            return errors;
        }
    }
}