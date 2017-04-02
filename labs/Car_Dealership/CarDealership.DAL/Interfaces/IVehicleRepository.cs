using CarDealership.Models;
using CarDealership.Models.Queries;
using System.Collections.Generic;

namespace CarDealership.DAL.Interfaces
{
    public interface IVehicleRepository
    {
        Vehicle GetById(int vehicleId);
        IEnumerable<VehicleDetail> GetAll();
        IEnumerable<VehicleDetail> GetBySearchTerm(string term);
        IEnumerable<VehicleDetail> GetByPriceRange(decimal min, decimal max);
        IEnumerable<VehicleDetail> GetByYearRange(int min, int max);
        void Delete(int vehicleId);
        void Insert(Vehicle vehicle);
        void Update(Vehicle vehicle);
    }
}
