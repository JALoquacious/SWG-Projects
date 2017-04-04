using CarDealership.DAL.Interfaces;
using CarDealership.DAL.Repositories.ADO;
using CarDealership.DAL.Repositories.Mock;
using System;

namespace CarDealership.DAL.Factories
{
    public class MakeRepositoryFactory
    {
        public static IMakeRepository GetRepository()
        {
            switch (Settings.GetRepositoryType())
            {
                case "PROD":
                    return new MakeRepositoryADO();
                case "QA":
                    return new MakeRepositoryMock();
                default:
                    throw new Exception("Could not find valid RepositoryType configuration value.");
            }
        }
    }
}
