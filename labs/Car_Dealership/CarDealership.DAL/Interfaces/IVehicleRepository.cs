using CarDealership.Models.Queries;
using CarDealership.Models.Tables;
using System.Collections.Generic;

namespace CarDealership.DAL.Interfaces
{
    public interface IVehicleRepository
    {
        VehicleDetail GetById(int vehicleId);
        IEnumerable<VehicleFeatured> GetFeatured();
        IEnumerable<VehicleDetail> GetAllDetails();
        IEnumerable<VehicleDetail> GetBySearchTerm(string term);
        IEnumerable<VehicleDetail> GetByPriceRange(decimal min, decimal max);
        IEnumerable<VehicleDetail> GetByYearRange(int min, int max);
        void Delete(int vehicleId);
        void Insert(Vehicle vehicle);
        void Update(Vehicle vehicle);
    }
}
