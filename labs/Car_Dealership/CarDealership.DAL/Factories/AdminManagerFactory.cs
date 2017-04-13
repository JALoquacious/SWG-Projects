using CarDealership.DAL.Interfaces;
using CarDealership.DAL.Repositories.ADO;
using CarDealership.DAL.Repositories.Mock;
using System;

namespace CarDealership.DAL.Factories
{
    public class AdminManagerFactory
    {
        public static IAdminManager GetRepository()
        {
            switch (Settings.GetRepositoryType())
            {
                case "PROD":
                    return new AdminManagerADO();
                case "QA":
                    return new AdminManagerMock();
                default:
                    throw new Exception("Could not find valid RepositoryType configuration value.");
            }
        }
    }
}
