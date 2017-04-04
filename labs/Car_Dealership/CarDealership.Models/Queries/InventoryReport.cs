namespace CarDealership.Models.Queries
{
    public class InventoryReport
    {
        public string Make        { get; set; }
        public string Model       { get; set; }
        public int Count          { get; set; }
        public int Year           { get; set; }
        public decimal StockValue { get; set; }
    }
}
