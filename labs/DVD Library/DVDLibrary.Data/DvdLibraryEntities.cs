using DVDLibrary.Models;
using System.Data.Entity;

namespace DVDLibrary.Data
{
    class DvdLibraryEntities : DbContext
    {
        public DvdLibraryEntities()
            : base("EFConnection")
        {
        }

        public DbSet<Dvd> Dvds { get; set; }
    }
}
