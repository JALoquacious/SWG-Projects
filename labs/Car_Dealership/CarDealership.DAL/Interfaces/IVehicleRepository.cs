using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
