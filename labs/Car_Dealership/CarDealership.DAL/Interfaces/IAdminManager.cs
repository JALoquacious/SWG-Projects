using CarDealership.Models.Queries;
using CarDealership.Models.Tables;
using System.Collections.Generic;

namespace CarDealership.DAL.Interfaces
{
    public interface IAdminManager
    {
        IEnumerable<SalesReport> GetSalesReport();
        IEnumerable<InventoryReport> GetInventoryReport();
        void Purchase(VehicleDetail vehicleDetail, Sale sale, Customer customer);
    }
}
