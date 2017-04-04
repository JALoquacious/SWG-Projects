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
            var vehicleDetails = repo.GetAll();

            Assert.IsNotNull(vehicleDetails);
            Assert.AreEqual(4, vehicleDetails.Count());
        }

        [TestCase(1, "Chevrolet", "Corvette", "Car", "DarkRed", "Charcoal", 2017)]
        [TestCase(2, "Toyota", "Tacoma", "Truck", "Tan", "Red", 1998)]
        [TestCase(3, "BMW", "X5", "SUV", "White", "Yellow", 2017)]
        [TestCase(4, "Honda", "Odyssey", "Van", "Black", "Black", 2010)]
        public void MockCanLoadVehicleById(int id, string expectedMake, string expectedModel,
            string expectedBodyStyle, string expectedInteriorColor, string expectedExteriorColor,
            int expectedYear)
        {
            var repo = new VehicleRepositoryMock();
            var vehicleDetail = repo.GetById(id);

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
            var vehicleDetails = repo.GetByPriceRange(15000, 50000).ToList();

            Assert.IsNotEmpty(vehicleDetails);
            Assert.AreEqual(2, vehicleDetails.Count());
            Assert.AreEqual(3, vehicleDetails[0].VehicleId);
            Assert.AreEqual(4, vehicleDetails[1].VehicleId);
        }

        [Test]
        public void MockCanLoadVehiclesByYearRange()
        {
            var repo = new VehicleRepositoryMock();
            var vehicleDetails = repo.GetByYearRange(1995, 2015).ToList();

            Assert.IsNotEmpty(vehicleDetails);
            Assert.AreEqual(2, vehicleDetails.Count());
            Assert.AreEqual(2, vehicleDetails[0].VehicleId);
            Assert.AreEqual(4, vehicleDetails[1].VehicleId);
        }

        [TestCase("20", 3)]
        [TestCase("2017", 2)]
        [TestCase("9", 1)]
        [TestCase("1", 4)]
        public void MockCanLoadVehiclesBySearchTermYear(string year, int expectedCount)
        {
            var repo = new VehicleRepositoryMock();
            var vehicleDetails = repo.GetBySearchTerm(year).ToList();

            Assert.IsNotEmpty(vehicleDetails);
            Assert.AreEqual(expectedCount, vehicleDetails.Count());
        }

        [TestCase("Chev", 1)]
        [TestCase("Toy", 2)]
        [TestCase("BM", 3)]
        [TestCase("Hon", 4)]
        public void MockCanLoadSingleVehicleBySearchTermMake(string make, int expectedId)
        {
            var repo = new VehicleRepositoryMock();
            var vehicleDetails = repo.GetBySearchTerm(make).ToList();

            Assert.IsNotEmpty(vehicleDetails);
            Assert.AreEqual(1, vehicleDetails.Count());
            Assert.AreEqual(expectedId, vehicleDetails[0].VehicleId);
        }

        [TestCase("Cor", 1)]
        [TestCase("Tac", 2)]
        [TestCase("X", 3)]
        [TestCase("Ody", 4)]
        public void MockCanLoadSingleVehicleBySearchTermModel(string model, int expectedId)
        {
            var repo = new VehicleRepositoryMock();
            var vehicleDetails = repo.GetBySearchTerm(model).ToList();

            Assert.IsNotEmpty(vehicleDetails);
            Assert.AreEqual(1, vehicleDetails.Count());
            Assert.AreEqual(expectedId, vehicleDetails[0].VehicleId);
        }

        [TestCase("a", 2)]
        [TestCase("t", 2)]
        [TestCase("c", 2)]
        [TestCase("o", 3)]
        public void MockCanLoadVehiclesBySearchTermAmbiguous(string term, int expectedCount)
        {
            var repo = new VehicleRepositoryMock();
            var vehicleDetails = repo.GetBySearchTerm(term).ToList();

            Assert.IsNotEmpty(vehicleDetails);
            Assert.AreEqual(expectedCount, vehicleDetails.Count());
        }
    }
}
