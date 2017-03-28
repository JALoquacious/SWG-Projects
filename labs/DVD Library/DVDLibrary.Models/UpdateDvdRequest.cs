using System.ComponentModel.DataAnnotations;

namespace DVDLibrary.Models
{
    public class UpdateDvdRequest
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title field is required.")]
        [RegularExpression(@"^[a-zA-Z,.'&\-\s]{1,50}$",
         ErrorMessage = "Title must be 1-50 characters containing only basic special characters.")]
        public string Title { get; set; }

        [RegularExpression(@"^$|^\d{4}$",
         ErrorMessage = "Year must be 4 digits.")]
        public int? ReleaseYear { get; set; }

        [RegularExpression(@"^$|^[a-zA-Z,.'&\-\s]{1,50}$",
         ErrorMessage = "Director field length must be < 50 normal characters.")]
        public string Director { get; set; }

        [RegularExpression(@"^$|^G|PG-13|PG|R|NC-17$",
         ErrorMessage = "Rating must be G, PG, PG-13, R, or NC-17.")]
        public string Rating { get; set; }

        [RegularExpression(@"^$|^.{1,500}$",
         ErrorMessage = "Notes field length must be < 500 characters.")]
        public string Notes { get; set; }
    }
}
