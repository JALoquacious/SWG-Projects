namespace Movie_Catalog_CodeFirst.Migrations
{
    using Models.EF;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Movie_Catalog_CodeFirst.Models.EF.MovieCatalogEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MovieCatalogEntities context)
        {
            context.Genres.AddOrUpdate(
                    g => g.GenreType,
                    new Genre { GenreType = "Sci-Fi" },
                    new Genre { GenreType = "Adventure" },
                    new Genre { GenreType = "Mystery" },
                    new Genre { GenreType = "Horror" }
                );

            context.Ratings.AddOrUpdate(
                r => r.RatingName,
                new Rating { RatingName = "G" },
                new Rating { RatingName = "PG" },
                new Rating { RatingName = "PG-13" },
                new Rating { RatingName = "R" }
            );

            context.SaveChanges();

            context.Movies.AddOrUpdate(
                m=>m.Title,
                new Movie {
                    Title ="Star Wars",
                    GenreId = context.Genres.First(g=>g.GenreType=="Sci-Fi").GenreId,
                    RatingId = context.Ratings.First(r => r.RatingName == "PG").RatingId
                }
            );
        }
    }
}
