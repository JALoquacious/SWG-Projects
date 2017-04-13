using CarDealership.DAL.Interfaces;
using CarDealership.Models.Queries;
using CarDealership.Models.Tables;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace CarDealership.DAL.Repositories.ADO
{
    public class AdminManagerADO : IAdminManager
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
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SaleInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter saleId = new SqlParameter("@SaleId", SqlDbType.Int);
                saleId.Direction = ParameterDirection.Output;

                SqlParameter customerId = new SqlParameter("@CustomerId", SqlDbType.Int);
                customerId.Direction = ParameterDirection.Output;

                // update Customer
                cmd.Parameters.Add(customerId);
                cmd.Parameters.AddWithValue("@UserId", "00000000-0000-0000-0000-000000000000");
                cmd.Parameters.AddWithValue("@Name", customer.Name);
                cmd.Parameters.AddWithValue("@Phone", customer.Phone);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@Street1", customer.Street1);
                cmd.Parameters.AddWithValue("@Street2", customer.Street2);
                cmd.Parameters.AddWithValue("@City", customer.City);
                cmd.Parameters.AddWithValue("@StateId", customer.State);
                cmd.Parameters.AddWithValue("@Zip", customer.Zip);

                // update Sale
                cmd.Parameters.Add(saleId);
                cmd.Parameters.AddWithValue("@PaymentTypeId", sale.PaymentTypeId);
                cmd.Parameters.AddWithValue("@PurchasePrice", sale.PurchasePrice);
                cmd.Parameters.AddWithValue("@Date", sale.Date);

                cmd.Parameters.AddWithValue("@VehicleId", vehicleDetail.VehicleId);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }
    }
}
