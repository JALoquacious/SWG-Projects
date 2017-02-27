using System.Configuration;
using FlooringBusiness.Data.TaxRepositories;
using FlooringBusiness.Models.Interfaces;

namespace FlooringBusiness.BLL.Factories
{
    public static class StateTaxRepositoryFactory
    {
        public static IStateTaxRepository Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"];

            switch (mode)
            {
                case "Testing":
                    return new TestStateTaxRepository();
                case "Production":
                    return new ProductionStateTaxRepository();
                default:
                    return null;
            }
        }
    }
}
