using CarDealership.DAL.Interfaces;
using CarDealership.DAL.Repositories.ADO;
using CarDealership.DAL.Repositories.Mock;
using System;

namespace CarDealership.DAL.Factories
{
    public class VehicleRepositoryFactory
    {
        public static IVehicleRepository GetRepository()
        {
            switch (Settings.GetRepositoryType())
            {
                case "PROD":
                    return new VehicleRepositoryADO();
                case "QA":
                    return new VehicleRepositoryMock();
                default:
                    throw new Exception("Could not find valid RepositoryType configuration value.");
            }
        }
    }
}
