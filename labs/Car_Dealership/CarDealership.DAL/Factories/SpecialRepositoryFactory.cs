using CarDealership.DAL.Interfaces;
using System;

namespace CarDealership.DAL.Factories
{
    public class SpecialRepositoryFactory
    {
        public static ISpecialRepository GetRepository()
        {
            switch (Settings.GetRepositoryType())
            {
                case "PROD":
                    return new SpecialRepositoryADO();
                case "QA":
                    return new SpecialRepositoryMock();
                default:
                    throw new Exception("Could not find valid RepositoryType configuration value.");
            }
        }
    }
}
