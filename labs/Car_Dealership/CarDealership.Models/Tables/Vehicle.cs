namespace CarDealership.Models
{
    public class Vehicle
    {
        public int VehicleId       { get; set; }
        public int UserId          { get; set; }
//      public int PurchaseId      { get; set; }
        public int ModelId         { get; set; }
//      public int TransmissionId  { get; set; }
        public int BodyStyleId     { get; set; }
        public int InteriorColorId { get; set; }
        public int ExteriorColorId { get; set; }
        public int Year            { get; set; }
        public bool IsUsed         { get; set; }
        public bool IsAutomatic    { get; set; }
        public bool IsFeatured     { get; set; }
        public string VIN          { get; set; }
        public string Description  { get; set; }
        public string Image        { get; set; }
        public decimal SalePrice   { get; set; }
        public decimal MSRP        { get; set; }
        public decimal Mileage     { get; set; }
    }
}
