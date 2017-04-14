using CarDealership.Models.Queries;
using System.Collections.Generic;

namespace CarDealership.UI.Models
{
    public class InventoryReportViewModel
    {
        public IEnumerable<InventoryReportQueryRow> NewInventory  { get; set; }
        public IEnumerable<InventoryReportQueryRow> UsedInventory { get; set; }
    }
}