using System;
using System.Collections.Generic;
using FlooringBusiness.Data.ProductRepositories;
using FlooringBusiness.Data.TaxRepositories;
using FlooringBusiness.Models.Interfaces;
using FlooringBusiness.Models.Prototypes;

namespace FlooringBusiness.Data.OrderRepositories
{
    public class TestOrderRepository : IOrderRepository
    {
        private static readonly Dictionary<DateTime, List<Order>> OrderMap = new Dictionary<DateTime, List<Order>>();
        private static readonly IStateTaxRepository StateTaxRepository = new TestStateTaxRepository();
        private static readonly IProductRepository ProductRepository = new TestProductRepository();

        static TestOrderRepository()
        {
            List<Order> orders1 = new List<Order>
            {
                new Order()
                {
                    OrderNumber = 1,
                    Customer    = "Microsoft",
                    Product     = ProductRepository.GetProduct("Carpet"),
                    Area        = 50.00m,
                    StateTax    = StateTaxRepository.GetStateTax("NY"),
                    Total       = (ProductRepository.GetProduct("Carpet").CostPerSquareFoot +
                                   ProductRepository.GetProduct("Carpet").LaborCostPerSquareFoot)
                                    * 50.00m
                                    * (1 + StateTaxRepository.GetStateTax("NY").TaxRate / 100)
                },
                new Order()
                {
                    OrderNumber = 2,
                    Customer    = "The Software Guild",
                    Product     = ProductRepository.GetProduct("Wood"),
                    Area        = 100.00m,
                    StateTax    = StateTaxRepository.GetStateTax("OH"),
                    Total       = (ProductRepository.GetProduct("Wood").CostPerSquareFoot +
                                   ProductRepository.GetProduct("Wood").LaborCostPerSquareFoot)
                                    * 100.00m
                                    * (1 + StateTaxRepository.GetStateTax("OH").TaxRate / 100)
                },
                new Order()
                {
                    OrderNumber = 3,
                    Customer    = "Google",
                    Product     = ProductRepository.GetProduct("Laminate"),
                    Area        = 150.00m,
                    StateTax    = StateTaxRepository.GetStateTax("AZ"),
                    Total       = (ProductRepository.GetProduct("Laminate").CostPerSquareFoot +
                                   ProductRepository.GetProduct("Laminate").LaborCostPerSquareFoot)
                                    * 150.00m
                                    * (1 + StateTaxRepository.GetStateTax("AZ").TaxRate / 100)
                },
                new Order()
                {
                    OrderNumber = 4,
                    Customer    = "Facebook",
                    Product     = ProductRepository.GetProduct("Tile"),
                    Area        = 200.00m,
                    StateTax    = StateTaxRepository.GetStateTax("AZ"),
                    Total       = (ProductRepository.GetProduct("Tile").CostPerSquareFoot +
                                   ProductRepository.GetProduct("Tile").LaborCostPerSquareFoot)
                                    * 200.00m
                                    * (1 + StateTaxRepository.GetStateTax("AZ").TaxRate / 100)
                },
                new Order()
                {
                    OrderNumber = 5,
                    Customer    = "Baidu",
                    Product     = ProductRepository.GetProduct("Laminate"),
                    Area        = 250.00m,
                    StateTax    = StateTaxRepository.GetStateTax("CA"),
                    Total       = (ProductRepository.GetProduct("Laminate").CostPerSquareFoot +
                                   ProductRepository.GetProduct("Laminate").LaborCostPerSquareFoot)
                                    * 250.00m
                                    * (1 + StateTaxRepository.GetStateTax("CA").TaxRate / 100)
                }
            };

            List<Order> orders2 = new List<Order>
            {
                new Order()
                {
                    OrderNumber = 1,
                    Customer    = "Facebook",
                    Product     = ProductRepository.GetProduct("Tile"),
                    Area        = 200.00m,
                    StateTax    = StateTaxRepository.GetStateTax("AZ"),
                    Total       = (ProductRepository.GetProduct("Tile").CostPerSquareFoot +
                                   ProductRepository.GetProduct("Tile").LaborCostPerSquareFoot)
                                    * 200.00m
                                    * (1 + StateTaxRepository.GetStateTax("AZ").TaxRate / 100)
                },
                new Order()
                {
                    OrderNumber = 2,
                    Customer    = "Baidu",
                    Product     = ProductRepository.GetProduct("Laminate"),
                    Area        = 250.00m,
                    StateTax    = StateTaxRepository.GetStateTax("CA"),
                    Total       = (ProductRepository.GetProduct("Laminate").CostPerSquareFoot +
                                   ProductRepository.GetProduct("Laminate").LaborCostPerSquareFoot)
                                    * 250.00m
                                    * (1 + StateTaxRepository.GetStateTax("CA").TaxRate / 100)
                }
            };
            OrderMap.Add(DateTime.Parse("1/1/2020"), orders1);
            OrderMap.Add(DateTime.Parse("2/2/2020"), orders2);
        }

        public List<Order> LoadOrders(DateTime date)
        {
            if (OrderMap.ContainsKey(date))
            {
                return OrderMap[date];
            }
            return null;
        }

        public void SaveOrders(DateTime date, List<Order> orders)
        {
            if (OrderMap.ContainsKey(date))
            {
                OrderMap[date] = orders;
            }
            else
            {
                OrderMap.Add(date, orders);
            }
        }

        public Product LoadProductData(string targetProduct)
        {
            return ProductRepository.GetProduct(targetProduct);
        }

        public StateTax LoadStateTaxData(string targetState)
        {
            return StateTaxRepository.GetStateTax(targetState);
        }
    }
}
