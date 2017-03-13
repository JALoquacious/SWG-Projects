using System.ComponentModel.DataAnnotations;

namespace IntroducingWebAPI.Models
{
    public class UpdateMovieRequest
    {
        [Required]
        public int MovieId { get; set; }
        
        [Required]
        public string Title { get; set; }

        [Required]
        public string Rating { get; set; }
    }
}