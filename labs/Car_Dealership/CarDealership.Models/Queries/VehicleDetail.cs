namespace CarDealership.Models.Queries
{
    public class VehicleDetail
    {
        public int VehicleId        { get; set; }
        public int UserId           { get; set; }
        public int Year             { get; set; }
        public bool IsUsed          { get; set; }
        public bool IsAutomatic     { get; set; }
        public bool IsFeatured      { get; set; }
        public string Make          { get; set; }
        public string Model         { get; set; }
        public string BodyStyle     { get; set; }
        public string InteriorColor { get; set; }
        public string ExteriorColor { get; set; }
        public string VIN           { get; set; }
        public string Description   { get; set; }
        public string Image         { get; set; }
        public decimal SalePrice    { get; set; }
        public decimal MSRP         { get; set; }
        public decimal Mileage      { get; set; }
    }
}
