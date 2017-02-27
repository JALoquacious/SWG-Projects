using System.Configuration;
using FlooringBusiness.Data.ProductRepositories;
using FlooringBusiness.Models.Interfaces;

namespace FlooringBusiness.BLL.Factories
{
    public static class ProductRepositoryFactory
    {
        public static IProductRepository Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"];

            switch (mode)
            {
                case "Testing":
                    return new TestProductRepository();
                case "Production":
                    return new ProductionProductRepository();
                default:
                    return null;
            }
        }
    }
}
