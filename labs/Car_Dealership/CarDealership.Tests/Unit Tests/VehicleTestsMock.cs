using CarDealership.DAL.Repositories.Mock;
using NUnit.Framework;
using System.Linq;

namespace CarDealership.Tests.Unit_Tests
{
    [TestFixture]
    public class VehicleTestsMock
    {
        [Test]
        public void MockCanLoadVehicles()
        {
            var repo = new VehicleRepositoryMock();
            var vehiclesDetail = repo.GetAllDetails();

            Assert.IsNotNull(vehiclesDetail);
            Assert.AreEqual(4, vehiclesDetail.Count());
        }

        [Test]
        public void MockCanLoadFeaturedVehicles()
        {
            var repo = new VehicleRepositoryMock();
            var vehiclesFeatured = repo.GetFeatured().ToList();

            Assert.IsNotNull(vehiclesFeatured);
            Assert.AreEqual(4, vehiclesFeatured.Count());
            Assert.AreEqual(11000m, vehiclesFeatured[0].SalePrice);
            Assert.AreEqual("BMW", vehiclesFeatured[1].Make);
            Assert.AreEqual("Frontier", vehiclesFeatured[2].Model);
        }

        [TestCase(1, "Chevrolet", "Corvette", "Car", "DarkRed", "Charcoal", 2017)]
        [TestCase(2, "Toyota", "Tacoma", "Truck", "Tan", "Red", 1998)]
        [TestCase(3, "BMW", "X5", "SUV", "White", "Yellow", 2017)]
        [TestCase(4, "Honda", "Odyssey", "Van", "Black", "Black", 2010)]
        public void MockCanLoadVehicleDetailById(int id, string expectedMake, string expectedModel,
            string expectedBodyStyle, string expectedInteriorColor, string expectedExteriorColor,
            int expectedYear)
        {
            var repo = new VehicleRepositoryMock();
            var vehicleDetail = repo.GetDetailById(id);

            Assert.IsNotNull(vehicleDetail);
            Assert.AreEqual(expectedModel, vehicleDetail.Model);
            Assert.AreEqual(expectedBodyStyle, vehicleDetail.BodyStyle);
            Assert.AreEqual(expectedExteriorColor, vehicleDetail.ExteriorColor);
            Assert.AreEqual(expectedInteriorColor, vehicleDetail.InteriorColor);
            Assert.AreEqual(expectedYear, vehicleDetail.Year);
        }

        [Test]
        public void MockCanLoadVehiclesByPriceRange()
        {
            var repo = new VehicleRepositoryMock();
            var vehiclesDetail = repo.GetByPriceRange(15000, 50000).ToList();

            Assert.IsNotEmpty(vehiclesDetail);
            Assert.AreEqual(2, vehiclesDetail.Count());
            Assert.AreEqual(3, vehiclesDetail[0].VehicleId);
            Assert.AreEqual(4, vehiclesDetail[1].VehicleId);
        }

        [Test]
        public void MockCanLoadVehiclesByYearRange()
        {
            var repo = new VehicleRepositoryMock();
            var vehiclesDetail = repo.GetByYearRange(1995, 2015).ToList();

            Assert.IsNotEmpty(vehiclesDetail);
            Assert.AreEqual(2, vehiclesDetail.Count());
            Assert.AreEqual(2, vehiclesDetail[0].VehicleId);
            Assert.AreEqual(4, vehiclesDetail[1].VehicleId);
        }

        [TestCase("20", 3)]
        [TestCase("2017", 2)]
        [TestCase("9", 1)]
        [TestCase("1", 4)]
        public void MockCanLoadVehiclesBySearchTermYear(string year, int expectedCount)
        {
            var repo = new VehicleRepositoryMock();
            var vehiclesDetail = repo.GetBySearchTerm(year).ToList();

            Assert.IsNotEmpty(vehiclesDetail);
            Assert.AreEqual(expectedCount, vehiclesDetail.Count());
        }

        [TestCase("Chev", 1)]
        [TestCase("Toy", 2)]
        [TestCase("BM", 3)]
        [TestCase("Hon", 4)]
        public void MockCanLoadSingleVehicleBySearchTermMake(string make, int expectedId)
        {
            var repo = new VehicleRepositoryMock();
            var vehiclesDetail = repo.GetBySearchTerm(make).ToList();

            Assert.IsNotEmpty(vehiclesDetail);
            Assert.AreEqual(1, vehiclesDetail.Count());
            Assert.AreEqual(expectedId, vehiclesDetail[0].VehicleId);
        }

        [TestCase("Cor", 1)]
        [TestCase("Tac", 2)]
        [TestCase("X", 3)]
        [TestCase("Ody", 4)]
        public void MockCanLoadSingleVehicleBySearchTermModel(string model, int expectedId)
        {
            var repo = new VehicleRepositoryMock();
            var vehiclesDetail = repo.GetBySearchTerm(model).ToList();

            Assert.IsNotEmpty(vehiclesDetail);
            Assert.AreEqual(1, vehiclesDetail.Count());
            Assert.AreEqual(expectedId, vehiclesDetail[0].VehicleId);
        }

        [TestCase("a", 2)]
        [TestCase("t", 2)]
        [TestCase("c", 2)]
        [TestCase("o", 3)]
        public void MockCanLoadVehiclesBySearchTermAmbiguous(string term, int expectedCount)
        {
            var repo = new VehicleRepositoryMock();
            var vehiclesDetail = repo.GetBySearchTerm(term).ToList();

            Assert.IsNotEmpty(vehiclesDetail);
            Assert.AreEqual(expectedCount, vehiclesDetail.Count());
        }
    }
}
