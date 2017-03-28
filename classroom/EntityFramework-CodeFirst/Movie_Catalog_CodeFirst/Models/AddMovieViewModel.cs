using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Movie_Catalog_CodeFirst.Models
{
    public class AddMovieViewModel
    {
        [Required(ErrorMessage = "Please enter a title.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please select a genre.")]
        public int SelectedGenreId { get; set; }

        public int? SelectedRatingId { get; set; }

        public IEnumerable<SelectListItem> Genres { get; set; }
        public IEnumerable<SelectListItem> Ratings { get; set; }
    }
}