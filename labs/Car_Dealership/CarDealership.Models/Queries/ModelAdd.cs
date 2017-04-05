using System;

namespace CarDealership.Models.Queries
{
    public class ModelAdd
    {
        public string Make        { get; set; }
        public string Model       { get; set; }
        public string User        { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
