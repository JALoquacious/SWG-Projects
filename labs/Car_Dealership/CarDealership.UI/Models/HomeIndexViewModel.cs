using CarDealership.Models.Queries;
using CarDealership.Models.Tables;
using System.Collections.Generic;

namespace CarDealership.UI.Models
{
    public class HomeIndexViewModel
    {
        public List<VehicleFeatured> Vehicles { get; set; }
        public List<Special> Specials         { get; set; }
    }
}