namespace CarDealership.Models.Queries
{
    public class VehicleSearchParameters
    {
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? MinYear      { get; set; }
        public int? MaxYear      { get; set; }
        public int Condition     { get; set; }
        public string SearchTerm { get; set; }
    }
}
