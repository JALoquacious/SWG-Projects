using CarDealership.DAL.Repositories.ADO;
using CarDealership.Models.Tables;
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

        [Test]
        public void ADOCanAddVehicle()
        {
            var repo = new VehicleRepositoryADO();
            var vehicle = new Vehicle();
            
            vehicle.ModelId         = 9;
            vehicle.BodyStyleId     = 4;
            vehicle.InteriorColorId = 2;
            vehicle.ExteriorColorId = 5;
            vehicle.IsUsed          = true;
            vehicle.IsAutomatic     = false;
            vehicle.IsFeatured      = false;
            vehicle.UserId          = "00000000-0000-0000-0000-000000000000";
            vehicle.VIN             = "9012345678ABCDEFG";
            vehicle.Description     = "Added vehicle";
            vehicle.Image           = "added.png";
            vehicle.SalePrice       = 15000m;
            vehicle.MSRP            = 16000m;
            vehicle.Mileage         = 90000m;

            repo.Insert(vehicle);

            Assert.IsNotNull(vehicle);
            Assert.AreEqual(3, vehicle.VehicleId);
        }

        [Test]
        public void ADOCanUpdateVehicle()
        {
            var vehicle = new Vehicle();
            var repo = new VehicleRepositoryADO();

            vehicle.ModelId         = 9;
            vehicle.BodyStyleId     = 4;
            vehicle.InteriorColorId = 2;
            vehicle.ExteriorColorId = 5;
            vehicle.IsUsed          = true;
            vehicle.IsAutomatic     = false;
            vehicle.IsFeatured      = false;
            vehicle.UserId          = "00000000-0000-0000-0000-000000000000";
            vehicle.VIN             = "9012345678ABCDEFG";
            vehicle.Description     = "Added vehicle";
            vehicle.Image           = "added.png";
            vehicle.SalePrice       = 15000m;
            vehicle.MSRP            = 16000m;
            vehicle.Mileage         = 90000m;

            repo.Insert(vehicle);

            vehicle.ModelId         = 28;
            vehicle.BodyStyleId     = 1;
            vehicle.InteriorColorId = 1;
            vehicle.ExteriorColorId = 4;
            vehicle.IsUsed          = false;
            vehicle.IsAutomatic     = true;
            vehicle.IsFeatured      = true;
            vehicle.UserId          = "11111111-1111-1111-1111-111111111111";
            vehicle.VIN             = "0123456789ABCDEFG";
            vehicle.Description     = "Updated vehicle";
            vehicle.Image           = "updated.png";
            vehicle.SalePrice       = 38000m;
            vehicle.MSRP            = 40000m;
            vehicle.Mileage         = 800m;

            repo.Update(vehicle);

            var updatedVehicle = repo.GetById(3);

            Assert.AreEqual("Tesla", updatedVehicle.Make);
            Assert.AreEqual("3", updatedVehicle.Model);
            Assert.AreEqual("Car", updatedVehicle.BodyStyle);
            Assert.AreEqual("Black", updatedVehicle.InteriorColor);
            Assert.AreEqual("Gray", updatedVehicle.ExteriorColor);
            Assert.AreEqual("11111111-1111-1111-1111-111111111111", updatedVehicle.UserId);
            Assert.AreEqual("0123456789ABCDEFG", updatedVehicle.VIN);
            Assert.AreEqual("Updated vehicle", updatedVehicle.Description);
            Assert.AreEqual("updated.png", updatedVehicle.Image);
            Assert.AreEqual(false, updatedVehicle.IsUsed);
            Assert.AreEqual(true, updatedVehicle.IsAutomatic);
            Assert.AreEqual(true, updatedVehicle.IsFeatured);
            Assert.AreEqual(2018, updatedVehicle.Year);
            Assert.AreEqual(38000m, updatedVehicle.SalePrice);
            Assert.AreEqual(40000m, updatedVehicle.MSRP);
            Assert.AreEqual(800m, updatedVehicle.Mileage);
        }

        [Test]
        public void ADOCanDeleteVehicle()
        {
            var repo = new VehicleRepositoryADO();
            var vehicle = new Vehicle();

            vehicle.ModelId = 9;
            vehicle.BodyStyleId = 4;
            vehicle.InteriorColorId = 2;
            vehicle.ExteriorColorId = 5;
            vehicle.IsUsed = true;
            vehicle.IsAutomatic = false;
            vehicle.IsFeatured = false;
            vehicle.UserId = "00000000-0000-0000-0000-000000000000";
            vehicle.VIN = "9012345678ABCDEFG";
            vehicle.Description = "Added vehicle";
            vehicle.Image = "added.png";
            vehicle.SalePrice = 15000m;
            vehicle.MSRP = 16000m;
            vehicle.Mileage = 90000m;

            repo.Insert(vehicle);

            var loaded = repo.GetById(3);
            Assert.IsNotNull(loaded);

            repo.Delete(3);
            loaded = repo.GetById(3);

            Assert.IsNull(loaded);
        }
    }
}