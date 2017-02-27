using System.Configuration;
using FlooringBusiness.Models.Interfaces;
using FlooringBusiness.Data.OrderRepositories;

namespace FlooringBusiness.BLL.Factories
{
    public static class OrderRepositoryFactory
    {
        public static IOrderRepository Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"];

            switch (mode)
            {
                case "Testing":
                    return new TestOrderRepository();
                case "Production":
                    return new ProductionOrderRepository();
                default:
                    return null;
            }
        }
    }
}
