namespace DVDLibrary.Data.Migrations
{
    using Models;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DVDLibrary.Data.DvdLibraryEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DVDLibrary.Data.DvdLibraryEntities context)
        {
            context.Dvds.AddOrUpdate(dvd => dvd.Title,
                new Dvd {
                    Title       = "Gladiator",
                    ReleaseYear = 2000,
                    Director    = "Ridley Scott",
                    Rating      = "R"
                },
                new Dvd {
                    Title       = "Tree of Life",
                    ReleaseYear = 2011,
                    Director    = "Terrence Malick",
                    Rating      = "PG-13"
                },
                new Dvd { Title = "Star Trek: First Contact",
                    ReleaseYear = 1996,
                    Director    = "Jonathan Frakes",
                    Rating      = "PG-13"
                }
            );
            context.SaveChanges();
        }
    }
}
