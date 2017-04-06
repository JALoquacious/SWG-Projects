using CarDealership.DAL.Interfaces;
using CarDealership.DAL.Repositories.Mock;
using System;

namespace CarDealership.DAL.Factories
{
    public class ContactRepositoryFactory
    {
        public static IContactRepository GetRepository()
        {
            switch (Settings.GetRepositoryType())
            {
                case "PROD":
                    return new ContactRepositoryADO();
                case "QA":
                    return new ContactRepositoryMock();
                default:
                    throw new Exception("Could not find valid RepositoryType configuration value.");
            }
        }
    }
}
