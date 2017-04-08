using CarDealership.UI.Models.CustomValidation;
using System.ComponentModel.DataAnnotations;

namespace CarDealership.UI.Models
{
    public class UserEditViewModel : UserAdminViewModel
    {
        public UserEditViewModel() : base() { }

        [DataType(DataType.Password)]
        [ValidOrNullPassword]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [ValidOrNullPassword]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}