using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Movie_Catalog_CodeFirst.Models.EF
{
    public class MovieCatalogEntities : DbContext
    {
        public MovieCatalogEntities()
            : base("MovieCatalog")
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Rating> Ratings { get; set; }
    }
}