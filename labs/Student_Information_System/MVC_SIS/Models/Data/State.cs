using Exercises.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Exercises.Models.Data
{
    public class State
    {
        [AbbreviationRule(ErrorMessage = "State Abbreviation must be 2 characters.")]
        [Required(ErrorMessage = "Please enter a State Abbreviation.")]
        public string StateAbbreviation { get; set; }

        [OnlyAlpha(ErrorMessage = "Only alphabetic characters are permitted.")]
        [Required(ErrorMessage = "Please enter a State Name.")]
        public string StateName { get; set; }
    }
}