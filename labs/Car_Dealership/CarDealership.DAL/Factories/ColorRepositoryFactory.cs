using CarDealership.DAL.Interfaces;
using CarDealership.DAL.Repositories.ADO;
using CarDealership.DAL.Repositories.Mock;
using System;

namespace CarDealership.DAL.Factories
{
    public class ColorRepositoryFactory
    {
        public static IColorRepository GetRepository()
        {
            switch (Settings.GetRepositoryType())
            {
                case "PROD":
                    return new ColorRepositoryADO();
                case "QA":
                    return new ColorRepositoryMock();
                default:
                    throw new Exception("Could not find valid RepositoryType configuration value.");
            }
        }
    }
}
