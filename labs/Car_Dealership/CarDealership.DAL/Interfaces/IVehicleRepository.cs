using CarDealership.Models.Queries;
using CarDealership.Models.Tables;
using System.Collections.Generic;

namespace CarDealership.DAL.Interfaces
{
    public interface IVehicleRepository
    {
        Vehicle GetById(int vehicleId);
        VehicleDetail GetDetailById(int vehicleId);
        IEnumerable<VehicleFeatured> GetFeatured();
        IEnumerable<VehicleDetail> GetAllDetails();
        IEnumerable<VehicleDetail> Search(VehicleSearchParameters parameters);
        void Delete(int vehicleId);
        void Insert(Vehicle vehicle);
        void Update(Vehicle vehicle);
        void Purchase(VehicleDetail vehicleDetail, Sale sale, Customer customer);
    }
}
