using CarDealership.DAL.Interfaces;
using CarDealership.Models.Tables;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CarDealership.DAL.Repositories.ADO
{
    public class StateRepositoryADO : IStateRepository
    {
        public IEnumerable<State> GetAll()
        {
            var states = new List<State>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var cmd = new SqlCommand("StatesSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var row = new State();
                        row.StateId = (int)dr["StateId"];
                        row.Name = dr["Name"].ToString();

                        states.Add(row);
                    }
                }
            }
            return states;
        }
    }
}
