using System;
using System.Collections.Generic;
using FlooringBusiness.Data.OrderRepositories;
using FlooringBusiness.Data.ProductRepositories;
using FlooringBusiness.Data.TaxRepositories;
using FlooringBusiness.Models.Interfaces;
using FlooringBusiness.Models.Prototypes;
using NUnit.Framework;

namespace FlooringBusiness.Tests
{
    [TestFixture]
    public class RepositoryTests
    {
        [TestCase("NY", "New York", 8.875, true)]
        [TestCase("OH", "Ohio", 6.25, true)]
        [TestCase("AZ", "Arizona", 5, true)]
        [TestCase("CA", "California", 9.75, true)]
        public void StateTaxRepoShouldReturnCorrectStateData(string expectedStateAbbreviation, string expectedStateName,
            decimal expectedTaxRate, bool expectedResult)
        {
            IStateTaxRepository stateTaxRepository = new TestStateTaxRepository();

            StateTax stateTaxFromAbbreviation = stateTaxRepository.GetStateTax(expectedStateAbbreviation);
            StateTax stateTaxFromFullName = stateTaxRepository.GetStateTax(expectedStateName);

            Assert.AreEqual(stateTaxFromAbbreviation, stateTaxFromFullName);
            Assert.AreEqual(expectedStateAbbreviation, stateTaxFromFullName.StateAbbreviation);
            Assert.AreEqual(expectedStateName, stateTaxFromFullName.StateName);
            Assert.AreEqual(expectedTaxRate, stateTaxFromFullName.TaxRate);
        }

        [TestCase("Carpet", 2.25, 2.1)]
        [TestCase("Laminate", 1.75, 2.1)]
        [TestCase("Tile", 3.5, 4.15)]
        [TestCase("Wood", 5.15, 4.75)]
        public void ProductRepoShouldReturnCorrectProductData(string expectedProductType, decimal expectedMaterialCost,
            decimal expectedLaborCost)
        {
            IProductRepository productRepository = new TestProductRepository();

            Product product = productRepository.GetProduct(expectedProductType);

            Assert.AreEqual(expectedProductType, product.ProductType);
            Assert.AreEqual(expectedMaterialCost, product.CostPerSquareFoot);
            Assert.AreEqual(expectedLaborCost, product.LaborCostPerSquareFoot);
        }

        [TestCase("Dirt")]
        [TestCase("Glass")]
        [TestCase("Leather")]
        public void ProductRepoShouldFailOnUnknownProduct(string productType)
        {
            IProductRepository productRepository = new TestProductRepository();

            Assert.IsNull(productRepository.GetProduct(productType));
        }

        [TestCase("Carpet", 3)]
        [TestCase("Laminate", 2)]
        [TestCase("Tile", 1)]
        [TestCase("Wood", 4)]
        public void ProductRepoShouldFailOnIncorrectMaterialCost(string productType, decimal materialCost)
        {
            IProductRepository productRepository = new TestProductRepository();

            Product product = productRepository.GetProduct(productType);
            
            Assert.AreNotEqual(materialCost, product.CostPerSquareFoot);
        }

        [TestCase("Carpet", 3)]
        [TestCase("Laminate", 2)]
        [TestCase("Tile", 1)]
        [TestCase("Wood", 4)]
        public void ProductRepoShouldFailOnIncorrectLaborCost(string productType, decimal laborCost)
        {
            IProductRepository productRepository = new TestProductRepository();

            Product product = productRepository.GetProduct(productType);

            Assert.AreNotEqual(laborCost, product.LaborCostPerSquareFoot);
        }

        [TestCase("1/1/2020", 1, "Microsoft", 50, 236.80)]
        [TestCase("1/1/2020", 2, "The Software Guild", 100, 1051.88)]
        [TestCase("2/2/2020", 2, "Baidu", 250, 1056.34)]
        public void OrderRepoShouldReturnCorrectOrderData(string expectedOrderDate, int expectedOrderNumber,
            string expectedCustomer, decimal expectedArea, decimal expectedTotal)
        {
            IOrderRepository orderRepository = new TestOrderRepository();

            List<Order> orders = orderRepository.LoadOrders(DateTime.Parse(expectedOrderDate));
            Order order = orders.Find(o => o.OrderNumber == expectedOrderNumber);

            Assert.IsNotEmpty(orders);
            Assert.AreEqual(expectedOrderNumber, order.OrderNumber);
            Assert.AreEqual(expectedCustomer, order.Customer);
            Assert.AreEqual((int) expectedArea, (int) order.Area);
            Assert.AreEqual((int) expectedTotal, (int) order.Total);
        }

        [TestCase("1/1/2020", 2, 333)]
        [TestCase("2/2/2020", 1, 999)]
        public void OrderRepoShouldFailOnIncorrectMaterialCost(string date, decimal orderNumber, decimal materialCost)
        {
            IOrderRepository orderRepository = new TestOrderRepository();

            List<Order> orders = orderRepository.LoadOrders(DateTime.Parse(date));
            Order order = orders.Find(o => o.OrderNumber == orderNumber);

            Assert.AreNotEqual(materialCost, order.MaterialCost);
        }

        [TestCase("1/1/2020", 2, 333)]
        [TestCase("2/2/2020", 1, 999)]
        public void OrderRepoShouldFailOnIncorrectLaborCost(string date, decimal orderNumber, decimal laborCost)
        {
            IOrderRepository orderRepository = new TestOrderRepository();

            List<Order> orders = orderRepository.LoadOrders(DateTime.Parse(date));
            Order order = orders.Find(o => o.OrderNumber == orderNumber);

            Assert.AreNotEqual(laborCost, order.MaterialCost);
        }

        [TestCase("5/5/2020")]
        [TestCase("7/7/2020")]
        public void OrderRepoShouldReturnNullOnUnknownDate(string expectedOrderDate)
        {
            IOrderRepository orderRepository = new TestOrderRepository();

            List<Order> orders = orderRepository.LoadOrders(DateTime.Parse(expectedOrderDate));

            Assert.IsNull(orders);
        }
    }
}
