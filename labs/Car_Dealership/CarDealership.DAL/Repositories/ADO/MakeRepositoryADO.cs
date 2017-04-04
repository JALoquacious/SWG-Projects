using CarDealership.DAL.Interfaces;
using CarDealership.Models.Tables;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CarDealership.DAL.Repositories.ADO
{
    public class MakeRepositoryADO : IMakeRepository
    {
        public IEnumerable<Make> GetAll()
        {
            var makes = new List<Make>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var cmd = new SqlCommand("MakesSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Make row   = new Make();
                        row.MakeId = (int)dr["MakeId"];
                        row.UserId = dr["UserId"].ToString();
                        row.Name   = dr["Name"].ToString();

                        makes.Add(row);
                    }
                }
            }
            return makes;
        }

        public Make GetById(int makeId)
        {
            Make make = null;

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var cmd = new SqlCommand("MakeSelectById", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MakeId", makeId);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        make        = new Make();
                        make.MakeId = (int)dr["MakeId"];
                        make.UserId = dr["UserId"].ToString();
                        make.Name   = dr["Name"].ToString();
                    }
                }
            }
            return make;
        }

        public void Insert(Make make)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("MakeInsert", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter param = new SqlParameter("@MakeId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@UserId", make.UserId);
                cmd.Parameters.AddWithValue("@Name", make.Name);
                cmd.Parameters.AddWithValue("@DateAdded", make.DateAdded);

                cn.Open();

                cmd.ExecuteNonQuery();

                make.MakeId = (int)param.Value;
            }
        }
    }
}
