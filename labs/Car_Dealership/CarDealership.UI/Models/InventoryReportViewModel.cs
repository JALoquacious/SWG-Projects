using CarDealership.Models.Queries;
using CarDealership.Models.Tables;

namespace CarDealership.UI.Models
{
    public class InventoryReportViewModel
    {
        public Make Make { get; set; }
        public Model Model { get; set; }
        public Vehicle Vehicle { get; set; }
        public InventoryReport InventoryReport { get; set; }
    }
}