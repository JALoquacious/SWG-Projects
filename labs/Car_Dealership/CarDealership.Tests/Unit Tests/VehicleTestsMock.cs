using CarDealership.DAL.Repositories.Mock;
using NUnit.Framework;
using System.Linq;

namespace CarDealership.Tests.Unit_Tests
{
    [TestFixture]
    public class VehicleTestsMock
    {
        [Test]
        public void CanLoadVehicles()
        {
            var repo = new VehicleRepositoryMock();
            var vehicleDetails = repo.GetAll();

            Assert.IsNotNull(vehicleDetails);
            Assert.AreEqual(4, vehicleDetails.Count());
        }

        [TestCase(1, 9, 1, 6, 5, 2017)]
        [TestCase(2, 24, 3, 4, 7, 1998)]
        [TestCase(3, 5, 2, 2, 9, 2017)]
        [TestCase(4, 15, 1, 1, 1, 2010)]
        public void CanLoadVehicleById(int id, int expectedModelId, int expectedBodyStyleId,
            int expectedInteriorColorId, int expectedExteriorColorId, int expectedYear)
        {
            var repo = new VehicleRepositoryMock();
            var vehicle = repo.GetById(id);

            Assert.IsNotNull(vehicle);
            Assert.AreEqual(expectedModelId, vehicle.ModelId);
            Assert.AreEqual(expectedBodyStyleId, vehicle.BodyStyleId);
            Assert.AreEqual(expectedExteriorColorId, vehicle.ExteriorColorId);
            Assert.AreEqual(expectedInteriorColorId, vehicle.InteriorColorId);
            Assert.AreEqual(expectedYear, vehicle.Year);
        }

        [Test]
        public void CanLoadVehiclesByPriceRange()
        {
            var repo = new VehicleRepositoryMock();
            var vehicleDetails = repo.GetByPriceRange(15000, 50000).ToList();

            Assert.IsNotEmpty(vehicleDetails);
            Assert.AreEqual(2, vehicleDetails.Count());
            Assert.AreEqual(3, vehicleDetails[0].VehicleId);
            Assert.AreEqual(4, vehicleDetails[1].VehicleId);
        }

        [Test]
        public void CanLoadVehiclesByYearRange()
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
        public void CanLoadVehiclesBySearchTermYear(string year, int expectedCount)
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
        public void CanLoadSingleVehicleBySearchTermMake(string make, int expectedId)
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
        public void CanLoadSingleVehicleBySearchTermModel(string model, int expectedId)
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
        public void CanLoadVehiclesBySearchTermAmbiguous(string term, int expectedCount)
        {
            var repo = new VehicleRepositoryMock();
            var vehicleDetails = repo.GetBySearchTerm(term).ToList();

            Assert.IsNotEmpty(vehicleDetails);
            Assert.AreEqual(expectedCount, vehicleDetails.Count());
        }
    }
}
