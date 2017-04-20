using CarDealership.UI.Models.CustomValidation;
using System.ComponentModel.DataAnnotations;

namespace CarDealership.UI.Models
{
    public class UserAddViewModel : UserAdminViewModel
    {
        public UserAddViewModel() : base() { }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [ValidPassword(ErrorMessage = "Password must be 5-50 characters and contain an uppercase letter, lowercase letter, and digit.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirmation password is required.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}