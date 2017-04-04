using CarDealership.DAL.Repositories.ADO;
using NUnit.Framework;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

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

        [Test]
        public void ADONotFoundVehicleReturnsNull()
        {
            var repo = new VehicleRepositoryADO();
            var vehicle = repo.GetById(100000);

            Assert.IsNull(vehicle);
        }

        [Test]
        public void CanLoadMakes()
        {
            var repo = new MakeRepositoryADO();
            var makes = repo.GetAll().ToList();

            Assert.AreEqual(8, makes.Count);
            Assert.AreEqual(1, makes[0].MakeId);
            Assert.AreEqual("Audi", makes[0].Name);
        }
    }
}
