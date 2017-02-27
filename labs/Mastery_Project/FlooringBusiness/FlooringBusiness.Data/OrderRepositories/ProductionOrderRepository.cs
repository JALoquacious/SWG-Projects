using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using FlooringBusiness.Data.ProductRepositories;
using FlooringBusiness.Data.TaxRepositories;
using FlooringBusiness.Models.Interfaces;
using FlooringBusiness.Models.Prototypes;

namespace FlooringBusiness.Data.OrderRepositories
{
    public class ProductionOrderRepository : IOrderRepository
    {
        private readonly ProductionProductRepository _productRepository = new ProductionProductRepository();
        private readonly ProductionStateTaxRepository _stateTaxRepository = new ProductionStateTaxRepository();
        private readonly ErrorLogWriter _errorLog = new ErrorLogWriter();
        private readonly string _filePath = ConfigurationManager.AppSettings["FilePath"] +@"Orders\";

        public List<Order> LoadOrders(DateTime date)
        {
            List<Order> orders = new List<Order>();
            Regex pattern = new Regex(@"\d\|[A-Za-z ]+\|[A-Za-z]{2}\|\d+(\.\d+)?\|[A-Za-z]+(\|\d+(\.\d+)?){7}");

            try
            {
                using (StreamReader stream = new StreamReader($"{_filePath}Orders_{date:MMddyyyy}.txt", false))
                {
                    while (!stream.EndOfStream)
                    {
                        string line = stream.ReadLine();
                        string[] columns = line.Split('|');
                        Order order = new Order();

                        if (line.StartsWith("State")) continue;

                        if (pattern.IsMatch(line))
                        {
                            order.OrderNumber = int.Parse(columns[0]);
                            order.Customer = columns[1];
                            order.StateTax = _stateTaxRepository.GetStateTax(columns[2]);
                            order.Product = _productRepository.GetProduct(columns[4]);
                            order.Area = decimal.Parse(columns[5]);
                            order.Total = decimal.Parse(columns[11]);

                            orders.Add(order);
                        }
                    }
                }
                return orders;
            }
            catch (IOException error)
            {
                _errorLog.Annotate($"{DateTime.Now} -- File I/O Error: {error.Message}");
                return null;
            }
            catch (FormatException error)
            {
                _errorLog.Annotate($"{DateTime.Now} -- Improper Format: {error.Message}");
                return null;
            }
            catch (Exception error)
            {
                _errorLog.Annotate($"{DateTime.Now} -- Unknown Error: {error.Message}");
                return null;
            }
        }

        public void SaveOrders(DateTime date, List<Order> orders)
        {
            try
            {
                using (StreamWriter stream = new StreamWriter($"{_filePath}Orders_{date:MMddyyyy}.txt", false))
                {
                    stream.WriteLine("OrderNumber|CustomerName|State|TaxRate|ProductType|Area|CostPerSquareFoot|" +
                                     "LaborCostPerSquareFoot|MaterialCost|LaborCost|Tax|Total");

                    foreach (Order o in orders)
                    {
                        o.MaterialCost = o.Product.CostPerSquareFoot * o.Area;
                        o.LaborCost = o.Product.LaborCostPerSquareFoot * o.Area;
                        decimal tax = (o.MaterialCost + o.LaborCost) * (1 + o.StateTax.TaxRate / 100);

                        stream.WriteLine(
                            $"{o.OrderNumber}|{o.Customer}|{o.StateTax.StateAbbreviation}|{o.StateTax.TaxRate:0.##}|" +
                            $"{o.Product.ProductType}|{o.Area:0.##}|{o.Product.CostPerSquareFoot:0.##}|" +
                            $"{o.Product.LaborCostPerSquareFoot:0.##}|{o.MaterialCost:0.##}|{o.LaborCost:0.##}|" +
                            $"{tax:0.##}|{o.Total:0.##}"
                        );
                    }
                }
            }
            catch (IOException error)
            {
                _errorLog.Annotate($"{DateTime.Now} -- File I/O Error: {error.Message}");
            }
            catch (FormatException error)
            {
                _errorLog.Annotate($"{DateTime.Now} -- Improper Format: {error.Message}");
            }
            catch (Exception error)
            {
                _errorLog.Annotate($"{DateTime.Now} -- Unknown Error: {error.Message}");
            }
        }

        public Product LoadProductData(string targetProduct)
        {
            return _productRepository.GetProduct(targetProduct);
        }

        public StateTax LoadStateTaxData(string targetState)
        {
            return _stateTaxRepository.GetStateTax(targetState);
        }
    }
}
