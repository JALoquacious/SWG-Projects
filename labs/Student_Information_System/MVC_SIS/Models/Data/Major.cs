using System.ComponentModel.DataAnnotations;

namespace Exercises.Models.Data
{
    public class Major
    {
        [Required(ErrorMessage = "Please enter a MAJOR.")]
        public int MajorId { get; set; }

        // must create new ViewModel to be able to use these attributes here
        //[Required(ErrorMessage = "Major name is required.")]
        //[StringLength(25, MinimumLength = 3, ErrorMessage = "Major name length must be between 3 and 25.")]
        //[OnlyAlpha(ErrorMessage = "Only alphabetic characters are permitted.")]
        public string MajorName { get; set; }
    }
}