using CarDealership.DAL.Interfaces;
using CarDealership.Models.Tables;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System;

namespace CarDealership.DAL.Factories
{
    public class SpecialRepositoryADO : ISpecialRepository
    {
        public void Delete(int specialId)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SpecialDelete", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SpecialId", specialId);

                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Special> GetAll()
        {
            var specials = new List<Special>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var cmd = new SqlCommand("SpecialsSelectAll", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var row         = new Special();
                        row.SpecialId   = (int)dr["SpecialId"];
                        row.Name        = dr["Name"].ToString();
                        row.Description = dr["Description"].ToString();

                        specials.Add(row);
                    }
                }
            }
            return specials;
        }

        public Special GetById(int specialId)
        {
            Special special = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var cmd = new SqlCommand("SpecialSelectById", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SpecialId", specialId);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        special             = new Special();
                        special.SpecialId   = (int)dr["SpecialId"];
                        special.Name        = dr["Name"].ToString();
                        special.Description = dr["Description"].ToString();
                    }
                }
            }
            return special;
        }

        public void Insert(Special special)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var cmd = new SqlCommand("SpecialInsert", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                var param = new SqlParameter("@SpecialId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@Name", special.Name);
                cmd.Parameters.AddWithValue("@Description", special.Description);

                cn.Open();

                cmd.ExecuteNonQuery();

                special.SpecialId = (int)param.Value;
            }
        }
    }
}