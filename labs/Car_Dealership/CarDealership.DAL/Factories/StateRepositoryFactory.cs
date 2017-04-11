using CarDealership.DAL.Interfaces;
using CarDealership.DAL.Repositories.ADO;
using CarDealership.DAL.Repositories.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.DAL.Factories
{
    public class StateRepositoryFactory
    {
        public static IStateRepository GetRepository()
        {
            switch (Settings.GetRepositoryType())
            {
                case "PROD":
                    return new StateRepositoryADO();
                case "QA":
                    return new StateRepositoryMock();
                default:
                    throw new Exception("Could not find valid RepositoryType configuration value.");
            }
        }
    }
}
