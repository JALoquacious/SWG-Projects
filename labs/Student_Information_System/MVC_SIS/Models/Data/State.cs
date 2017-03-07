using Exercises.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Exercises.Models.Data
{
    public class State
    {
        [Required(ErrorMessage = "Please enter a State.")]
        [AbbreviationLength(ErrorMessage = "State Abbreviation must be 2 characters.")]
        [OnlyAlpha(ErrorMessage = "Only alphabetic characters are permitted.")]
        public string StateAbbreviation { get; set; }

        // must create new ViewModel to be able to use these attributes here
        //[Required(ErrorMessage = "Please enter a State Name.")]
        //[StringLength(25, MinimumLength = 2, ErrorMessage = "State name length must be between 2 and 25.")]
        //[OnlyAlpha(ErrorMessage = "Only alphabetic characters are permitted.")]
        public string StateName { get; set; }
    }
}