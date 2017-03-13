using System.ComponentModel.DataAnnotations;

namespace IntroducingWebAPI.Models
{
    public class AddMovieRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Rating { get; set; }
    }
}