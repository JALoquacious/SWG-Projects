using System;
using System.Collections.Generic;
using FlooringBusiness.Models.Interfaces;
using FlooringBusiness.Models.Prototypes;

namespace FlooringBusiness.Data.ProductRepositories
{
    public class TestProductRepository : IProductRepository
    {
        private static readonly List<Product> Products;

        static TestProductRepository()
        {
            Products = new List<Product>();

            Product carpet = new Product()
            {
                ProductType            = "Carpet",
                CostPerSquareFoot      = 2.25m,
                LaborCostPerSquareFoot = 2.1m
            };

            Product laminate = new Product()
            {
                ProductType            = "Laminate",
                CostPerSquareFoot      = 1.75m,
                LaborCostPerSquareFoot = 2.1m
            };

            Product tile = new Product()
            {
                ProductType            = "Tile",
                CostPerSquareFoot      = 3.5m,
                LaborCostPerSquareFoot = 4.15m
            };

            Product wood = new Product()
            {
                ProductType            = "Wood",
                CostPerSquareFoot      = 5.15m,
                LaborCostPerSquareFoot = 4.75m
            };

            Products.Add(carpet);
            Products.Add(laminate);
            Products.Add(tile);
            Products.Add(wood);
        }

        public Product GetProduct(String name)
        {
            return Products.Find(p => String.Equals(p.ProductType, name, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
