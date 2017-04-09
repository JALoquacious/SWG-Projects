using CarDealership.DAL.Interfaces;
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
