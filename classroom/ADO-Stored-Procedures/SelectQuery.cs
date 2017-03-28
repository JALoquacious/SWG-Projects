using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace _02B_ADO_Stored_Procedures
{
    public class SelectQuery
    {
        public List<EmployeePayRate> GetEmployeeRates()
        {
            List<EmployeePayRate> rates = new List<EmployeePayRate>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString =
                    ConfigurationManager.ConnectionStrings["SWCCorp"].ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "EmployeeRatesSelect";

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        EmployeePayRate currentRow = new EmployeePayRate();

                        currentRow.FirstName = dr["FirstName"].ToString();
                        currentRow.LastName = dr["LastName"].ToString();

                        if (dr["HourlyRate"] != DBNull.Value)
                            currentRow.HourlyRate = (decimal)dr["HourlyRate"];

                        if (dr["MonthlySalary"] != DBNull.Value)
                            currentRow.MonthlySalary = (decimal)dr["MonthlySalary"];

                        if (dr["YearlySalary"] != DBNull.Value)
                            currentRow.YearlySalary = (decimal)dr["YearlySalary"];

                        currentRow.EmpId = (int)dr["EmpId"];
                        rates.Add(currentRow);
                    }
                }
            }

            return rates;
        }
    }
}
