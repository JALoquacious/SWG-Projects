using NUnit.Framework;
using System.Configuration;
using System.Data.SqlClient;

namespace CarDealership.Tests.Integration_Tests
{
    [TestFixture]
    public class VehicleTestsADO
    {
        [SetUp]
        public void Init()
        {
            using (var cn = new SqlConnection(
                ConfigurationManager
                    .ConnectionStrings["DefaultConnection"]
                    .ConnectionString
                )
            )
            {
                var cmd = new SqlCommand()
                {
                    CommandText = "DbReset",
                    CommandType = System.Data.CommandType.StoredProcedure,
                    Connection = cn
                };

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
