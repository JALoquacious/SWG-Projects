using CarDealership.DAL.Interfaces;
using CarDealership.Models.Queries;
using CarDealership.Models.Tables;
using System.Collections.Generic;
using System.Linq;

namespace CarDealership.DAL.Repositories.Mock
{
    public class VehicleRepositoryMock : IVehicleRepository
    {
        private static List<Vehicle> _vehicles;
        private static List<VehicleDetail> _vehiclesDetail;
        private static List<VehicleFeatured> _vehiclesFeatured;

        static VehicleRepositoryMock()
        {
            // Load static Vehicle objects
            _vehicles = new List<Vehicle>()
            {
                new Vehicle()
                {
                    VehicleId = 1,
                    UserId = "123",
                    ModelId = 9,
                    BodyStyleId = 1,
                    InteriorColorId = 6,
                    ExteriorColorId = 5,
                    IsUsed = false,
                    IsAutomatic = false,
                    IsFeatured = true,
                    VIN = "1234567890ABCDEFG",
                    Description = "This one will make everyone look.",
                    Image = "Images/vehicle9.png",
                    SalePrice = 54000m,
                    MSRP = 55450m,
                    Mileage = 110m
                },
                new Vehicle()
                {
                    VehicleId = 2,
                    UserId = "234",
                    ModelId = 24,
                    BodyStyleId = 3,
                    InteriorColorId = 4,
                    ExteriorColorId = 7,
                    IsUsed = true,
                    IsAutomatic = false,
                    IsFeatured = true,
                    VIN = "2345678901ABCDEFG",
                    Description = "A rugged, rusty, reliable old friend.",
                    Image = "Images/vehicle24.png",
                    SalePrice = 2800,
                    MSRP = 3000,
                    Mileage = 350000m
                },
                new Vehicle()
                {
                    VehicleId = 3,
                    UserId = "345",
                    ModelId = 5,
                    BodyStyleId = 2,
                    InteriorColorId = 2,
                    ExteriorColorId = 9,
                    IsUsed = false,
                    IsAutomatic = true,
                    IsFeatured = true,
                    VIN = "3456789012ABCDEFG",
                    Description = "Luxury and sport, all in one package.",
                    Image = "Images/vehicle5.png",
                    SalePrice = 47000m,
                    MSRP = 48400m,
                    Mileage = 212m
                },
                new Vehicle()
                {
                    VehicleId = 4,
                    UserId = "456",
                    ModelId = 15,
                    BodyStyleId = 1,
                    InteriorColorId = 1,
                    ExteriorColorId = 1,
                    IsUsed = true,
                    IsAutomatic = true,
                    IsFeatured = true,
                    VIN = "4567890123ABCDEFG",
                    Description = "Something for the whole family.",
                    Image = "Images/vehicle15.png",
                    SalePrice = 18000m,
                    MSRP = 20000m,
                    Mileage = 105000m
                }
            };

            // Load static VehicleDetail objects
            _vehiclesDetail = new List<VehicleDetail>()
            {
                new VehicleDetail()
                {
                    VehicleId = 1,
                    Make = "Chevrolet",
                    Model = "Corvette",
                    BodyStyle = "Car",
                    InteriorColor = "DarkRed",
                    ExteriorColor = "Charcoal",
                    Year = 2017,
                    IsUsed = false,
                    IsAutomatic = false,
                    IsFeatured = true,
                    VIN = "1234567890ABCDEFG",
                    Description = "This one will make everyone look.",
                    Image = "Images/vehicle9.png",
                    SalePrice = 54000m,
                    MSRP = 55450m,
                    Mileage = 110m
                },
                new VehicleDetail()
                {
                    VehicleId = 2,
                    Make = "Toyota",
                    Model = "Tacoma",
                    BodyStyle = "Truck",
                    InteriorColor = "Tan",
                    ExteriorColor = "Red",
                    Year = 1998,
                    IsUsed = true,
                    IsAutomatic = false,
                    IsFeatured = true,
                    VIN = "2345678901ABCDEFG",
                    Description = "A rugged, rusty, reliable old friend.",
                    Image = "Images/vehicle24.png",
                    SalePrice = 2800,
                    MSRP = 3000,
                    Mileage = 350000m
                },
                new VehicleDetail()
                {
                    VehicleId = 3,
                    Make = "BMW",
                    Model = "X5",
                    BodyStyle = "SUV",
                    InteriorColor = "White",
                    ExteriorColor = "Yellow",
                    Year = 2017,
                    IsUsed = false,
                    IsAutomatic = true,
                    IsFeatured = false,
                    VIN = "3456789012ABCDEFG",
                    Description = "Luxury and sport, all in one package.",
                    Image = "Images/vehicle5.png",
                    SalePrice = 47000m,
                    MSRP = 48400m,
                    Mileage = 212m
                },
                new VehicleDetail()
                {
                    VehicleId = 4,
                    Make = "Honda",
                    Model = "Odyssey",
                    BodyStyle = "Van",
                    InteriorColor = "Black",
                    ExteriorColor = "Black",
                    Year = 2010,
                    IsUsed = true,
                    IsAutomatic = true,
                    IsFeatured = false,
                    VIN = "4567890123ABCDEFG",
                    Description = "Something for the whole family.",
                    Image = "Images/vehicle15.png",
                    SalePrice = 18000m,
                    MSRP = 20000m,
                    Mileage = 105000m
                }
            };

            _vehiclesFeatured = new List<VehicleFeatured>()
            {
                new VehicleFeatured()
                {
                    VehicleId = 1,
                    Year = 2000,
                    Make = "Aston Martin",
                    Model = "DB9",
                    Image = "aston_martin_db9.png",
                    SalePrice = 100000m
                },
                new VehicleFeatured()
                {
                    VehicleId = 2,
                    Year = 2010,
                    Make = "Buick",
                    Model = "Encore",
                    Image = "buick_encore.png",
                    SalePrice = 15000m
                },
                new VehicleFeatured()
                {
                    VehicleId = 3,
                    Year = 2017,
                    Make = "Volkswagen",
                    Model = "Passat",
                    Image = "volkswagen_passat.png",
                    SalePrice = 23000m
                },
            };
        }

        public void Delete(int targetId)
        {
            _vehicles.RemoveAll(v => v.VehicleId == targetId);
        }

        public IEnumerable<VehicleDetail> GetAllDetails()
        {
            return _vehiclesDetail;
        }

        public VehicleDetail GetById(int targetId)
        {
            return _vehiclesDetail.FirstOrDefault(v => v.VehicleId == targetId);
        }

        public IEnumerable<VehicleDetail> GetBySearchTerm(string term)
        {
            return _vehiclesDetail.Where(v =>
                v.Year.ToString().Contains(term) ||
                v.Make.ToUpper().Contains(term.ToUpper()) ||
                v.Model.ToUpper().Contains(term.ToUpper())
            );
        }

        public IEnumerable<VehicleDetail> GetByPriceRange(decimal min, decimal max)
        {
            return _vehiclesDetail.Where(v => v.SalePrice >= min && v.SalePrice <= max);
        }

        public IEnumerable<VehicleDetail> GetByYearRange(int min, int max)
        {
            return _vehiclesDetail.Where(v => v.Year >= min && v.Year <= max);
        }

        public void Insert(Vehicle targetVehicle)
        {
            if (_vehicles.Any())
            {
                targetVehicle.VehicleId = _vehicles.Max(v => v.VehicleId) + 1;
            }
            else
            {
                targetVehicle.VehicleId = 1;
            }
            _vehicles.Add(targetVehicle);
        }

        public void Update(Vehicle targetVehicle)
        {
            _vehicles.RemoveAll(v => v.VehicleId == targetVehicle.VehicleId);
            _vehicles.Add(targetVehicle);
        }

        public IEnumerable<VehicleFeatured> GetFeatured()
        {
            return _vehiclesFeatured;
        }
    }
}
