namespace CarDealership.Models.Tables
{
    public class Communication
    {
        public int CommunicationId { get; set; }
        public int VehicleId       { get; set; }
        public int ContactId       { get; set; }
        public string Message      { get; set; }
    }
}
