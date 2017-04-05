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
        public void ADOCanLoadVehicleDetail()
        {
            var repo = new VehicleRepositoryADO();
            var vehicle = repo.GetById(1);

            Assert.IsNotNull(vehicle);
            Assert.AreEqual(1, vehicle.VehicleId);
            Assert.AreEqual(2017, vehicle.Year);
            Assert.AreEqual(false, vehicle.IsUsed);
            Assert.AreEqual(true, vehicle.IsAutomatic);
            Assert.AreEqual(true, vehicle.IsFeatured);
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", vehicle.UserId);
            Assert.AreEqual("Audi", vehicle.Make);
            Assert.AreEqual("A4", vehicle.Model);
            Assert.AreEqual("Car", vehicle.BodyStyle);
            Assert.AreEqual("Tan", vehicle.InteriorColor);
            Assert.AreEqual("Blue", vehicle.ExteriorColor);
            Assert.AreEqual(32000M, vehicle.SalePrice);
            Assert.AreEqual(34900M, vehicle.MSRP);
            Assert.AreEqual(236M, vehicle.Mileage);
            Assert.AreEqual("1234567890ABCDEFG", vehicle.VIN);
            Assert.AreEqual("A shiny new Audi sedan, just waiting for you.", vehicle.Description);
            Assert.AreEqual("audi_a4.png", vehicle.Image);
        }

        [Test]
        public void ADOCanLoadVehiclesFeatured()
        {
            var repo = new VehicleRepositoryADO();
            var vehiclesFeatured = repo.GetFeatured().ToList();

            Assert.IsNotNull(vehiclesFeatured);
            Assert.AreEqual(1, vehiclesFeatured.Count());
            Assert.AreEqual(1, vehiclesFeatured[0].VehicleId);
            Assert.AreEqual(2017, vehiclesFeatured[0].Year);
            Assert.AreEqual("Audi", vehiclesFeatured[0].Make);
            Assert.AreEqual("A4", vehiclesFeatured[0].Model);
            Assert.AreEqual("audi_a4.png", vehiclesFeatured[0].Image);
            Assert.AreEqual(32000m, vehiclesFeatured[0].SalePrice);
        }

        [Test]
        public void ADOCanLoadAllVehiclesDetail()
        {
            var repo = new VehicleRepositoryADO();
            var vehiclesDetail = repo.GetAllDetails().ToList();

            Assert.IsNotNull(vehiclesDetail);
            Assert.AreEqual(2, vehiclesDetail.Count());
            Assert.AreEqual(236, vehiclesDetail[0].Mileage);
            Assert.AreEqual(false, vehiclesDetail[1].IsFeatured);
            Assert.AreEqual("SUV", vehiclesDetail[1].BodyStyle);
            Assert.AreEqual(true, vehiclesDetail[0].IsAutomatic);
        }
    }
}