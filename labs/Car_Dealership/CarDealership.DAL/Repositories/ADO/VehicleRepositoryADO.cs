using CarDealership.DAL.Interfaces;
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

        public IEnumerable<VehicleDetail> GetAll()
        {
            var vehicles = new List<VehicleDetail>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ListingsSelectFront", cn);
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

                        if (dr["VehicleDescription"] != DBNull.Value)
                            row.Description = dr["Description"].ToString();

                        if (dr["ImageFileName"] != DBNull.Value)
                            row.Image = dr["Image"].ToString();

                        vehicles.Add(row);
                    }
                }
            }
            return vehicles;
        }

        public VehicleDetail GetById(int vehicleId)
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
                        vehicle = new VehicleDetail()
                        {
                            VehicleId     = (int)dr["VehicleId"],
                            IsUsed        = (bool)dr["IsUsed"],
                            IsAutomatic   = (bool)dr["IsAutomatic"],
                            IsFeatured    = (bool)dr["IsFeatured"],
                            SalePrice     = (decimal)dr["SalePrice"],
                            MSRP          = (decimal)dr["MSRP"],
                            Mileage       = (decimal)dr["Mileage"],
                            Model         = dr["Model"].ToString(),
                            BodyStyle     = dr["BodyStyle"].ToString(),
                            InteriorColor = dr["InteriorColor"].ToString(),
                            ExteriorColor = dr["ExteriorColor"].ToString(),
                            UserId        = dr["UserId"].ToString(),
                            VIN           = dr["VIN"].ToString()
                        };
                        if (dr["Description"] != DBNull.Value)
                            vehicle.Description = dr["Description"].ToString();

                        if (dr["Image"] != DBNull.Value)
                            vehicle.Image = dr["Image"].ToString();
                    }
                }
            }
            return vehicle;
        }

        public IEnumerable<VehicleDetail> GetBySearchTerm(string term)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VehicleDetail> GetByPriceRange(decimal min, decimal max)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VehicleDetail> GetByYearRange(int min, int max)
        {
            throw new NotImplementedException();
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
                cmd.Parameters.AddWithValue("@Image"          , vehicle.Image);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }
    }
}
