using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CarDealership.UI.Models
{
    public class UserAdminViewModel
    {
        public UserAdminViewModel()
        {
            Roles = new List<SelectListItem>()
            {
                new SelectListItem() { Value = "Admin"   , Text = "Admin"    },
                new SelectListItem() { Value = "Sales"   , Text = "Sales"    },
                new SelectListItem() { Value = "Disabled", Text = "Disabled" }
            };
        }

        public ApplicationUser User { get; set; }

        public List<SelectListItem> Roles { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "First name is required.")]
        [RegularExpression(@"^[A-Za-z\- \']*$",
            ErrorMessage = "Only letters, hyphens, apostrophes, and spaces allowed for first name.")]
        [StringLength(30, ErrorMessage = "First name must be fewer than 30 characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Last name is required.")]
        [RegularExpression(@"^[A-Za-z\- \']*$",
            ErrorMessage = "Only letters, hyphens, apostrophes, and spaces allowed for last name.")]
        [StringLength(30, ErrorMessage = "First name must be fewer than 30 characters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress (ErrorMessage = "Invalid email address.")]
        [StringLength(50, ErrorMessage = "Email must be fewer than 50 characters.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public string Role { get; set; }
    }
}