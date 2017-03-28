using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movie_Catalog_CodeFirst.Models.EF
{
    public class Movie
    {
        public int MovieId { get; set; }
        public int GenreId { get; set; }
        public int? RatingId { get; set; }
        public string Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? DirectorId { get; set; }

        public virtual Director Director { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual Rating Rating { get; set; }
    }
}