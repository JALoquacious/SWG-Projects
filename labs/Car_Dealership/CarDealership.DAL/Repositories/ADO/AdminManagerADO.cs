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
        public IEnumerable<UserReportQueryRow> GetUserReport()
        {
            var users = new List<UserReportQueryRow>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var cmd = new SqlCommand("UserReport", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var row       = new UserReportQueryRow();
                        row.FirstName = dr["FirstName"].ToString();
                        row.LastName  = dr["LastName"].ToString();
                        row.Email     = dr["Email"].ToString();
                        row.Role      = dr["Role"].ToString();
                        row.Id        = dr["Id"].ToString();

                        users.Add(row);
                    }
                }
            }
            return users;
        }

        public IEnumerable<InventoryReportQueryRow> GetInventoryReport(bool isUsed)
        {
            var inventories = new List<InventoryReportQueryRow>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var cmd = new SqlCommand("InventoryReport", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IsUsed", isUsed);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var row        = new InventoryReportQueryRow();
                        row.Year       = (int)dr["Year"];
                        row.Count      = (int)dr["Count"];
                        row.StockValue = (decimal)dr["StockValue"];
                        row.Make       = dr["Make"].ToString();
                        row.Model      = dr["Model"].ToString();

                        inventories.Add(row);
                    }
                }
            }
            return inventories;
        }

        public IEnumerable<SalesReportQueryRow> GetSalesReport()
        {
            var sales = new List<SalesReportQueryRow>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var cmd = new SqlCommand("SalesReport", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var row           = new SalesReportQueryRow();
                        row.TotalVehicles = (int)dr["TotalVehicles"];
                        row.TotalSales    = (decimal)dr["TotalSales"];
                        row.UserName      = dr["UserName"].ToString();

                        sales.Add(row);
                    }
                }
            }
            return sales;
        }

        public IEnumerable<SalesReportQueryRow> FilterSalesReport(string user, DateTime? fromDate, DateTime? toDate)
        {
            var sales = new List<SalesReportQueryRow>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                string query = (
                    @"SELECT U.UserName
		                ,SUM(S.PurchasePrice) AS TotalSales
                        ,COUNT(V.VehicleId) AS TotalVehicles
                    FROM Sales AS S
                    INNER JOIN Vehicles AS V ON S.SaleId = V.SaleId
                    INNER JOIN AspNetUsers AS U ON U.Id = V.UserId
                    WHERE 1 = 1
                ");

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                if (!string.IsNullOrEmpty(user))
                {
                    query += "AND U.Id = @UserId ";
                    cmd.Parameters.AddWithValue("@UserId", user);
                }

                if (fromDate.HasValue)
                {
                    query += "AND S.Date >= @FromDate ";
                    cmd.Parameters.AddWithValue("@FromDate", fromDate.Value);
                }

                if (toDate.HasValue)
                {
                    query += "AND S.Date <= @ToDate ";
                    cmd.Parameters.AddWithValue("@ToDate", toDate.Value);
                }

                query += "GROUP BY U.UserName ";
                query += "ORDER BY TotalSales DESC";

                cmd.CommandText = query;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var row = new SalesReportQueryRow();

                        row.TotalVehicles = (int)dr["TotalVehicles"];
                        row.TotalSales    = (decimal)dr["TotalSales"];
                        row.UserName      = dr["UserName"].ToString();

                        sales.Add(row);
                    }
                }
            }
            return sales;
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
                cmd.Parameters.AddWithValue("@UserId", customer.UserId);
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
                // update Vehicle
                cmd.Parameters.AddWithValue("@VehicleId", vehicleDetail.VehicleId);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }
    }
}
