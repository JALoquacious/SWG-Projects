using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CarDealership.UI.Models
{
    public class UserAddViewModel
    {
        public UserAddViewModel()
        {
            Roles = new List<SelectListItem>()
            {
                new SelectListItem() { Value = "admin"   , Text = "Admin"    },
                new SelectListItem() { Value = "sales"   , Text = "Sales"    },
                new SelectListItem() { Value = "disabled", Text = "Disabled" },
            };
        }

        public List<SelectListItem> Roles { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [RegularExpression(@"^[A-Za-z\- \']*$",
            ErrorMessage = "Only letters, hyphens, apostrophes, and spaces allowed for first name.")]
        [StringLength(30, ErrorMessage = "First name must be fewer than 30 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [RegularExpression(@"^[A-Za-z\- \']*$",
            ErrorMessage = "Only letters, hyphens, apostrophes, and spaces allowed for last name.")]
        [StringLength(30, ErrorMessage = "First name must be fewer than 30 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        [StringLength(30, ErrorMessage = "Email must be fewer than 50 characters.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public string Role { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}