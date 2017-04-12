using System.Collections.Generic;
using System.Web.Mvc;

namespace CarDealership.Models.Queries
{
    public class VehicleSearchParameters
    {
        public VehicleSearchParameters()
        {
            var tiers = new int[] { 2500, 5000, 7500, 10000, 15000, 20000, 25000, 30000, 35000,
                                    40000, 45000, 50000, 60000, 70000, 80000, 90000, 100000 };

            PriceList = new List<SelectListItem>();
            foreach (int price in tiers)
            {
                PriceList.Add(
                    new SelectListItem()
                    {
                        Text  = price.ToString($"$###,###"),
                        Value = price.ToString()
                    }
                );
            }
        }

        public decimal? MinPrice              { get; set; }
        public decimal? MaxPrice              { get; set; }
        public int? MinYear                   { get; set; }
        public int? MaxYear                   { get; set; }
        public int? Condition                 { get; set; }
        public bool IsAspNetUser              { get; set; }
        public string SearchTerm              { get; set; }
        public List<SelectListItem> PriceList { get; set; }
    }
}
