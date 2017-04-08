using CarDealership.UI.Models.CustomValidation;
using System.ComponentModel.DataAnnotations;

namespace CarDealership.UI.Models
{
    public class UserAddViewModel : UserAdminViewModel
    {
        public UserAddViewModel() : base() { }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [ValidPassword]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirmation password is required.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [ValidPassword]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}