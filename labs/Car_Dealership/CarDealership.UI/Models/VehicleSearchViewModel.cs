using CarDealership.Models.Queries;
using System.Collections.Generic;

namespace CarDealership.UI.Models
{
    public class VehicleSearchViewModel
    {
        public VehicleSearchViewModel()
        {
            SearchParams  = new VehicleSearchParameters();
            SearchResults = null;
        }

        public VehicleSearchParameters SearchParams { get; set; }
        public List<VehicleDetail> SearchResults    { get; set; }
    }
}