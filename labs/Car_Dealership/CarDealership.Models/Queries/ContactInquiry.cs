namespace CarDealership.Models.Queries
{
    public class ContactInquiry
    {
        public int ContactId  { get; set; }
        public int VehicleId  { get; set; }
        public string UserId  { get; set; }
        public string Name    { get; set; }
        public string Phone   { get; set; }
        public string Email   { get; set; }
        public string Message { get; set; }
        public string VIN     { get; set; }
    }
}
