using Microsoft.AspNet.Identity.EntityFramework;

namespace Movie_Catalog.Models.Identity
{
    public class MovieCatalogDbContext : IdentityDbContext<AppUser>
    {
        public MovieCatalogDbContext() : base("MovieCatalog")
        {

        }
    }
}