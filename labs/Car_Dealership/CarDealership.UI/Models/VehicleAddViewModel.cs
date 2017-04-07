using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealership.UI.Models
{
    public class VehicleAddViewModel : IValidatableObject
    {
        public Make Make                          { get; set; }
        public Model Model                        { get; set; }
        public Vehicle Vehicle                    { get; set; }
        public HttpPostedFileBase ImageUpload     { get; set; }
        public IEnumerable<SelectListItem> Makes  { get; set; }
        public IEnumerable<SelectListItem> Models { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (Model.Year < 2000 || Model.Year > DateTime.Today.Year + 1)
            {
                errors.Add(new ValidationResult("Year must be between 2000 and next year."));
            }

            if (Vehicle.Mileage < 0m || Vehicle.Mileage > 1000000m)
            {
                errors.Add(new ValidationResult("Mileage must be between 0 and 1,000,000."));
            }

            if (Vehicle.MSRP <= 0m || Vehicle.MSRP >= 1000000m)
            {
                errors.Add(new ValidationResult("MSRP must be between 0 and $1,000,000."));
            }

            if (Vehicle.SalePrice <= 0m || Vehicle.SalePrice >= 1000000m)
            {
                errors.Add(new ValidationResult("Sale price must be between 0 and $1,000,000."));
            }

            if (Vehicle.VIN.Length != 17)
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