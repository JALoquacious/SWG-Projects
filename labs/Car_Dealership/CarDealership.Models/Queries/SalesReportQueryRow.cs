namespace CarDealership.Models.Queries
{
    public class SalesReportQueryRow
    {
        public string UserName    { get; set; }
        public string Email       { get; set; }
        public decimal TotalSales { get; set; }
        public int TotalVehicles  { get; set; }
    }
}
