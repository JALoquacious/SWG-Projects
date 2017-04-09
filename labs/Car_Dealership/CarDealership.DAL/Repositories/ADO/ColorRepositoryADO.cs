using CarDealership.DAL.Interfaces;
using CarDealership.Models.Tables;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CarDealership.DAL.Repositories.ADO
{
    public class ColorRepositoryADO : IColorRepository
    {
        public IEnumerable<ExteriorColor> GetAllExterior()
        {
            var exteriorColors = new List<ExteriorColor>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var cmd = new SqlCommand("ExteriorColorsSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var row = new ExteriorColor();
                        row.ExteriorColorId = (int)dr["ExteriorColorId"];
                        row.Name = dr["Name"].ToString();

                        exteriorColors.Add(row);
                    }
                }
            }
            return exteriorColors;
        }

        public IEnumerable<InteriorColor> GetAllInterior()
        {
            var interiorColors = new List<InteriorColor>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var cmd = new SqlCommand("InteriorColorsSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var row = new InteriorColor();
                        row.InteriorColorId = (int)dr["InteriorColorId"];
                        row.Name = dr["Name"].ToString();

                        interiorColors.Add(row);
                    }
                }
            }
            return interiorColors;
        }
    }
}