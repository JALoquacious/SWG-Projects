using CarDealership.Models.Queries;
using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;

namespace CarDealership.DAL.Interfaces
{
    public interface IAdminManager
    {
        IEnumerable<SalesReportQueryRow> GetSalesReport();
        IEnumerable<SalesReportQueryRow> FilterSalesReport(string user, DateTime? fromDate, DateTime? toDate);
        IEnumerable<InventoryReportQueryRow> GetInventoryReport(bool isUsed);
        void Purchase(VehicleDetail vehicleDetail, Sale sale, Customer customer);
    }
}
