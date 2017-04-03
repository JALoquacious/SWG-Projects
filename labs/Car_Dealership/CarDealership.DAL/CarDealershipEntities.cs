using CarDealership.Models.Tables;
using System.Data.Entity;

namespace CarDealership.DAL
{
    public class CarDealershipEntities : DbContext
    {
        public CarDealershipEntities()
            : base("DefaultConnection")
        {
        }
        public DbSet<BodyStyle> BodyStyles         { get; set; }
        public DbSet<Communication> Communications { get; set; }
        public DbSet<Contact> Contacts             { get; set; }
        public DbSet<Customer> Customers           { get; set; }
        public DbSet<ExteriorColor> ExteriorColors { get; set; }
        public DbSet<InteriorColor> InteriorColors { get; set; }
        public DbSet<Make> Makes                   { get; set; }
        public DbSet<Model> Models                 { get; set; }
        public DbSet<PaymentType> PaymentTypes     { get; set; }
        public DbSet<Sale> Sales                   { get; set; }
        public DbSet<Salesperson> Salespersons     { get; set; }
        public DbSet<State> States                 { get; set; }
        public DbSet<Vehicle> Vehicles             { get; set; }
    }
}
