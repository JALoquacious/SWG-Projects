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
        void Delete(int vehicleId);
        void Insert(Vehicle vehicle);
        void Update(Vehicle vehicle);
    }
}
