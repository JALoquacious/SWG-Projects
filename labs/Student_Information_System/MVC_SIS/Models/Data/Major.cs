using Exercises.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Exercises.Models.Data
{
    public class Major
    {
        [Required(ErrorMessage = "Please enter a MAJOR.")]
        public int MajorId { get; set; }
        [OnlyAlpha(ErrorMessage = "Only alphabetic characters are permitted.")]
        public string MajorName { get; set; }
    }
}