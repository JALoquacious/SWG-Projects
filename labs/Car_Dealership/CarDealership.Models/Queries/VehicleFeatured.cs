namespace CarDealership.Models.Queries
{
    public class VehicleFeatured
    {
        public int VehicleId     { get; set; }
        public int Year          { get; set; }
        public string Make       { get; set; }
        public string Model      { get; set; }
        public string Image      { get; set; }
        public decimal SalePrice { get; set; }
    }
}
