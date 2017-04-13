using System;
using System.Collections.Generic;
using CarDealership.DAL.Interfaces;
using CarDealership.Models.Queries;
using CarDealership.Models.Tables;

namespace CarDealership.DAL.Repositories.Mock
{
    public class AdminManagerMock : IAdminManager
    {
        public IEnumerable<InventoryReport> GetInventoryReport()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SalesReport> GetSalesReport()
        {
            throw new NotImplementedException();
        }

        public void Purchase(VehicleDetail vehicleDetail, Sale sale, Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
