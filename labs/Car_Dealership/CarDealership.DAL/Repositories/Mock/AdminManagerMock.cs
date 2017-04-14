using System;
using System.Collections.Generic;
using CarDealership.DAL.Interfaces;
using CarDealership.Models.Queries;
using CarDealership.Models.Tables;

namespace CarDealership.DAL.Repositories.Mock
{
    public class AdminManagerMock : IAdminManager
    {
        public IEnumerable<SalesReportQueryRow> FilterSalesReport(string user, DateTime? fromDate, DateTime? toDate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<InventoryReportQueryRow> GetInventoryReport(bool isUsed)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SalesReportQueryRow> GetSalesReport()
        {
            throw new NotImplementedException();
        }

        public void Purchase(VehicleDetail vehicleDetail, Sale sale, Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
