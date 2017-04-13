using CarDealership.DAL.Interfaces;
using CarDealership.Models.Enums;
using CarDealership.Models.Queries;
using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CarDealership.DAL.Repositories.ADO
{
    public class VehicleRepositoryADO : IVehicleRepository
    {
        public void Delete(int vehicleId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehicleDelete", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@VehicleId", vehicleId);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<VehicleDetail> GetAllDetails()
        {
            var vehicles = new List<VehicleDetail>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehiclesSelectDetails", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var row = new VehicleDetail()
                        {
                            VehicleId     = (int)dr["VehicleId"],
                            Year          = (int)dr["Year"],
                            IsUsed        = (bool)dr["IsUsed"],
                            IsAutomatic   = (bool)dr["IsAutomatic"],
                            IsFeatured    = (bool)dr["IsFeatured"],
                            SalePrice     = (decimal)dr["SalePrice"],
                            MSRP          = (decimal)dr["MSRP"],
                            Mileage       = (decimal)dr["Mileage"],
                            Make          = dr["Make"].ToString(),
                            Model         = dr["Model"].ToString(),
                            BodyStyle     = dr["BodyStyle"].ToString(),
                            InteriorColor = dr["InteriorColor"].ToString(),
                            ExteriorColor = dr["ExteriorColor"].ToString(),
                            UserId        = dr["UserId"].ToString(),
                            VIN           = dr["VIN"].ToString()
                        };

                        if (dr["SaleId"] != DBNull.Value)
                            row.SaleId = (int)dr["SaleId"];

                        if (dr["Description"] != DBNull.Value)
                            row.Description = dr["Description"].ToString();

                        if (dr["Image"] != DBNull.Value)
                            row.Image = dr["Image"].ToString();

                        vehicles.Add(row);
                    }
                }
            }
            return vehicles;
        }


        public Vehicle GetById(int vehicleId)
        {
            Vehicle vehicle = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehicleSelect", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VehicleId", vehicleId);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        vehicle = new Vehicle();

                        vehicle.VehicleId       = (int)dr["VehicleId"];
                        vehicle.ModelId         = (int)dr["ModelId"];
                        vehicle.BodyStyleId     = (int)dr["BodyStyleId"];
                        vehicle.InteriorColorId = (int)dr["InteriorColorId"];
                        vehicle.ExteriorColorId = (int)dr["ExteriorColorId"];
                        vehicle.IsAutomatic     = (bool)dr["IsUsed"];
                        vehicle.IsUsed          = (bool)dr["IsAutomatic"];
                        vehicle.IsFeatured      = (bool)dr["IsFeatured"];
                        vehicle.SalePrice       = (decimal)dr["SalePrice"];
                        vehicle.MSRP            = (decimal)dr["MSRP"];
                        vehicle.Mileage         = (decimal)dr["Mileage"];
                        vehicle.UserId          = dr["UserId"].ToString();
                        vehicle.VIN             = dr["VIN"].ToString();

                        if (dr["SaleId"] != DBNull.Value)
                            vehicle.SaleId = (int)dr["SaleId"];

                        if (dr["Description"] != DBNull.Value)
                            vehicle.Description = dr["Description"].ToString();

                        if (dr["Image"] != DBNull.Value)
                            vehicle.Image = dr["Image"].ToString();
                    }
                }
            }

            return vehicle;
        }

        public VehicleDetail GetDetailById(int vehicleId)
        {
            VehicleDetail vehicle = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehicleSelectDetails", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VehicleId", vehicleId);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        vehicle               = new VehicleDetail();
                        vehicle.VehicleId     = (int)dr["VehicleId"];
                        vehicle.Year          = (int)dr["Year"];
                        vehicle.IsUsed        = (bool)dr["IsUsed"];
                        vehicle.IsAutomatic   = (bool)dr["IsAutomatic"];
                        vehicle.IsFeatured    = (bool)dr["IsFeatured"];
                        vehicle.SalePrice     = (decimal)dr["SalePrice"];
                        vehicle.MSRP          = (decimal)dr["MSRP"];
                        vehicle.Mileage       = (decimal)dr["Mileage"];
                        vehicle.Make          = dr["Make"].ToString();
                        vehicle.Model         = dr["Model"].ToString();
                        vehicle.BodyStyle     = dr["BodyStyle"].ToString();
                        vehicle.InteriorColor = dr["InteriorColor"].ToString();
                        vehicle.ExteriorColor = dr["ExteriorColor"].ToString();
                        vehicle.UserId        = dr["UserId"].ToString();
                        vehicle.VIN           = dr["VIN"].ToString();

                        if (dr["SaleId"] != DBNull.Value)
                            vehicle.SaleId = (int)dr["SaleId"];

                        if (dr["Description"] != DBNull.Value)
                            vehicle.Description = dr["Description"].ToString();

                        if (dr["Image"] != DBNull.Value)
                            vehicle.Image = dr["Image"].ToString();
                    }
                }
            }
            return vehicle;
        }

        public void Insert(Vehicle vehicle)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehicleInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@VehicleId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@SaleId"         , DBNull.Value);
                cmd.Parameters.AddWithValue("@UserId"         , vehicle.UserId);
                cmd.Parameters.AddWithValue("@ModelId"        , vehicle.ModelId);
                cmd.Parameters.AddWithValue("@BodyStyleId"    , vehicle.BodyStyleId);
                cmd.Parameters.AddWithValue("@InteriorColorId", vehicle.InteriorColorId);
                cmd.Parameters.AddWithValue("@ExteriorColorId", vehicle.ExteriorColorId);
                cmd.Parameters.AddWithValue("@SalePrice"      , vehicle.SalePrice);
                cmd.Parameters.AddWithValue("@MSRP"           , vehicle.MSRP);
                cmd.Parameters.AddWithValue("@Mileage"        , vehicle.Mileage);
                cmd.Parameters.AddWithValue("@VIN"            , vehicle.VIN);
                cmd.Parameters.AddWithValue("@Description"    , vehicle.Description);
                cmd.Parameters.AddWithValue("@IsUsed"         , vehicle.IsUsed);
                cmd.Parameters.AddWithValue("@IsAutomatic"    , vehicle.IsAutomatic);
                cmd.Parameters.AddWithValue("@IsFeatured"     , vehicle.IsFeatured);

                if (string.IsNullOrEmpty(vehicle.Image))
                {
                    cmd.Parameters.AddWithValue("@Image", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Image", vehicle.Image);
                }

                cn.Open();

                cmd.ExecuteNonQuery();

                vehicle.VehicleId = (int)param.Value;
            }
        }

        public void Update(Vehicle vehicle)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehicleUpdate", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@VehicleId"      , vehicle.VehicleId);
                cmd.Parameters.AddWithValue("@SaleId"         , vehicle.SaleId);
                cmd.Parameters.AddWithValue("@UserId"         , vehicle.UserId);
                cmd.Parameters.AddWithValue("@ModelId"        , vehicle.ModelId);
                cmd.Parameters.AddWithValue("@BodyStyleId"    , vehicle.BodyStyleId);
                cmd.Parameters.AddWithValue("@InteriorColorId", vehicle.InteriorColorId);
                cmd.Parameters.AddWithValue("@ExteriorColorId", vehicle.ExteriorColorId);
                cmd.Parameters.AddWithValue("@SaleId"         , vehicle.SaleId);
                cmd.Parameters.AddWithValue("@SalePrice"      , vehicle.SalePrice);
                cmd.Parameters.AddWithValue("@MSRP"           , vehicle.MSRP);
                cmd.Parameters.AddWithValue("@Mileage"        , vehicle.Mileage);
                cmd.Parameters.AddWithValue("@VIN"            , vehicle.VIN);
                cmd.Parameters.AddWithValue("@Description"    , vehicle.Description);
                cmd.Parameters.AddWithValue("@IsUsed"         , vehicle.IsUsed);
                cmd.Parameters.AddWithValue("@IsAutomatic"    , vehicle.IsAutomatic);
                cmd.Parameters.AddWithValue("@IsFeatured"     , vehicle.IsFeatured);
                cmd.Parameters.AddWithValue("@Image"          , vehicle.Image);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
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
                cmd.Parameters.AddWithValue("@UserId" , "00000000-0000-0000-0000-000000000000");
                cmd.Parameters.AddWithValue("@Name"   , customer.Name);
                cmd.Parameters.AddWithValue("@Phone"  , customer.Phone);
                cmd.Parameters.AddWithValue("@Email"  , customer.Email);
                cmd.Parameters.AddWithValue("@Street1", customer.Street1);
                cmd.Parameters.AddWithValue("@Street2", customer.Street2);
                cmd.Parameters.AddWithValue("@City"   , customer.City);
                cmd.Parameters.AddWithValue("@StateId", customer.State);
                cmd.Parameters.AddWithValue("@Zip"    , customer.Zip);

                // update Sale
                cmd.Parameters.Add(saleId);
                cmd.Parameters.AddWithValue("@PaymentTypeId", sale.PaymentTypeId);
                cmd.Parameters.AddWithValue("@PurchasePrice", sale.PurchasePrice);
                cmd.Parameters.AddWithValue("@Date"         , sale.Date);

                cmd.Parameters.AddWithValue("@VehicleId", vehicleDetail.VehicleId);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<VehicleFeatured> GetFeatured()
        {
            var vehiclesFeatured = new List<VehicleFeatured>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("VehiclesSelectFeatured", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var row       = new VehicleFeatured();
                        row.VehicleId = (int)dr["VehicleId"];
                        row.Year      = (int)dr["Year"];
                        row.SalePrice = (decimal)dr["SalePrice"];
                        row.Make      = dr["Make"].ToString();
                        row.Model     = dr["Model"].ToString();

                        if (dr["Image"] != DBNull.Value)
                            row.Image = dr["Image"].ToString();

                        vehiclesFeatured.Add(row);
                    }
                }
            }
            return vehiclesFeatured;
        }

        public IEnumerable<VehicleDetail> Search(VehicleSearchParameters parameters)
        {
            var vehicles = new List<VehicleDetail>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                string query = (
                    @"SELECT TOP 20 VehicleId
	                    ,V.UserId
	                    ,[Year]
	                    ,IsUsed
	                    ,IsAutomatic
	                    ,IsFeatured
	                    ,MK.[Name] AS Make
	                    ,MD.[Name] AS Model
	                    ,BS.[Name] AS BodyStyle
	                    ,IC.[Name] AS InteriorColor
	                    ,EC.[Name] AS ExteriorColor
	                    ,VIN
	                    ,V.[Description]
	                    ,[Image]
                        ,SaleId
	                    ,SalePrice
	                    ,MSRP
	                    ,Mileage
                    FROM Vehicles V
                    INNER JOIN BodyStyles BS ON BS.BodyStyleId = V.BodyStyleId
                    INNER JOIN InteriorColors IC ON IC.InteriorColorId = V.InteriorColorId
                    INNER JOIN ExteriorColors EC ON EC.ExteriorColorId = V.ExteriorColorId
                    INNER JOIN Models MD ON MD.ModelId = V.ModelId
                    INNER JOIN Makes MK ON MK.MakeId = MD.MakeId
                    WHERE 1 = 1 
                ");

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                if (parameters.IsAspNetUser)
                {
                    query += "AND SaleId IS NULL ";
                    cmd.Parameters.AddWithValue("@IsAspNetUser", parameters.IsAspNetUser);
                }

                if (parameters.Condition == (int)Condition.New ||
                    parameters.Condition == (int)Condition.Used)
                {
                    query += "AND IsUsed = @Condition ";
                    cmd.Parameters.AddWithValue("@Condition", parameters.Condition);
                }

                if (parameters.MinPrice.HasValue)
                {
                    query += "AND SalePrice >= @MinPrice ";
                    cmd.Parameters.AddWithValue("@MinPrice", parameters.MinPrice.Value);
                }

                if (parameters.MaxPrice.HasValue)
                {
                    query += "AND SalePrice <= @MaxPrice ";
                    cmd.Parameters.AddWithValue("@MaxPrice", parameters.MaxPrice.Value);
                }

                if (parameters.MinYear.HasValue)
                {
                    query += "AND [Year] >= @MinYear ";
                    cmd.Parameters.AddWithValue("@MinYear", parameters.MinYear.Value);
                }

                if (parameters.MaxYear.HasValue)
                {
                    query += "AND [Year] <= @MaxYear ";
                    cmd.Parameters.AddWithValue("@MaxYear", parameters.MaxYear.Value);
                }

                if (!string.IsNullOrEmpty(parameters.SearchTerm))
                {
                    query += @"AND MK.[Name] LIKE '%' + @SearchTerm + '%'
                        OR MD.[Name] LIKE '%' + @SearchTerm + '%'
                        OR [Year] LIKE '%' + @SearchTerm + '%'";
                    cmd.Parameters.AddWithValue("@SearchTerm", parameters.SearchTerm);
                }

                query += "ORDER BY MSRP DESC";

                cmd.CommandText = query;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var row = new VehicleDetail();

                        row.VehicleId     = (int)dr["VehicleId"];
                        row.Year          = (int)dr["Year"];
                        row.IsUsed        = (bool)dr["IsUsed"];
                        row.IsAutomatic   = (bool)dr["IsAutomatic"];
                        row.IsFeatured    = (bool)dr["IsFeatured"];
                        row.SalePrice     = (decimal)dr["SalePrice"];
                        row.MSRP          = (decimal)dr["MSRP"];
                        row.Mileage       = (decimal)dr["Mileage"];
                        row.Make          = dr["Make"].ToString();
                        row.Model         = dr["Model"].ToString();
                        row.BodyStyle     = dr["BodyStyle"].ToString();
                        row.InteriorColor = dr["InteriorColor"].ToString();
                        row.ExteriorColor = dr["ExteriorColor"].ToString();
                        row.UserId        = dr["UserId"].ToString();
                        row.VIN           = dr["VIN"].ToString();

                        if (dr["SaleId"] != DBNull.Value)
                            row.SaleId = (int)dr["SaleId"];

                        if (dr["Description"] != DBNull.Value)
                            row.Description = dr["Description"].ToString();

                        if (dr["Image"] != DBNull.Value)
                            row.Image = dr["Image"].ToString();

                        vehicles.Add(row);
                    }
                }
            }
            return vehicles;
        }
    }
}
