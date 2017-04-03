using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.DAL.Repositories.ADO
{
    public class VehicleRepositoryADO : IVehicleRepository
    {
        public void Delete(int vehicleId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VehicleDetail> GetAll()
        {
            throw new NotImplementedException();
        }

        public Vehicle GetById(int vehicleId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VehicleDetail> GetBySearchTerm(string term)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VehicleDetail> GetByPriceRange(decimal min, decimal max)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VehicleDetail> GetByYearRange(int min, int max)
        {
            throw new NotImplementedException();
        }

        public void Insert(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }

        public void Update(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }
    }
}
