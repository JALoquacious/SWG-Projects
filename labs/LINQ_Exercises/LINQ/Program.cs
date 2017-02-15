using LINQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ
{
    class Program
    {
        static void Main()
        {
            //PrintAllProducts();
            //PrintAllCustomers();
            //Exercise1();
            //Exercise2();
            //Exercise3();
            //Exercise4();
            //Exercise5();
            //Exercise6();
            //Exercise7();
            //Exercise8();
            //Exercise9();
            Exercise10();
            //Exercise11();
            //Exercise12();
            //Exercise13();
            //Exercise14();
            //Exercise15();
            //Exercise16();
            //Exercise17();
            //Exercise18();
            //Exercise19();
            //Exercise20();
            //Exercise21();
            //Exercise22();
            //Exercise23();
            //Exercise24();
            //Exercise25();
            //Exercise26();
            //Exercise27();
            //Exercise28();
            //Exercise29();
            //Exercise30();
            //Exercise31();

            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        #region "Sample Code"
        /// <summary>
        /// Sample, load and print all the product objects
        /// </summary>
        static void PrintAllProducts()
        {
            List<Product> products = DataLoader.LoadProducts();
            PrintProductInformation(products);
        }

        /// <summary>
        /// This will print a nicely formatted list of products
        /// </summary>
        /// <param name="products">The collection of products to print</param>
        static void PrintProductInformation(IEnumerable<Product> products)
        {
            string line = "{0,-5} {1,-35} {2,-15} {3,6:c} {4,6}";
            Console.WriteLine(line, "ID", "Product Name", "Category", "Unit", "Stock");
            Console.WriteLine("==============================================================================");

            foreach (var product in products)
            {
                Console.WriteLine(line, product.ProductID, product.ProductName, product.Category,
                    product.UnitPrice, product.UnitsInStock);
            }

        }

        /// <summary>
        /// Sample, load and print all the customer objects and their orders
        /// </summary>
        static void PrintAllCustomers()
        {
            var customers = DataLoader.LoadCustomers();
            PrintCustomerInformation(customers);
        }

        /// <summary>
        /// This will print a nicely formated list of customers
        /// </summary>
        /// <param name="customers">The collection of customer objects to print</param>
        static void PrintCustomerInformation(IEnumerable<Customer> customers)
        {
            foreach (var customer in customers)
            {
                Console.WriteLine("==============================================================================");
                Console.WriteLine(customer.CompanyName);
                Console.WriteLine(customer.Address);
                Console.WriteLine("{0}, {1} {2} {3}", customer.City, customer.Region, customer.PostalCode, customer.Country);
                Console.WriteLine("p:{0} f:{1}", customer.Phone, customer.Fax);
                Console.WriteLine();
                Console.WriteLine("\tOrders");
                foreach (var order in customer.Orders)
                {
                    Console.WriteLine("\t{0} {1:MM-dd-yyyy} {2,10:c}", order.OrderID, order.OrderDate, order.Total);
                }
                Console.WriteLine("==============================================================================");
            }
        }
        #endregion

        /// <summary>
        /// Print all products that are out of stock.
        /// </summary>
        static void Exercise1()
        {
            List<Product> products = DataLoader.LoadProducts();
            var query = products.Where(p => p.UnitsInStock == 0);

            PrintProductInformation(query);
        }

        /// <summary>
        /// Print all products that are in stock and cost more than 3.00 per unit.
        /// </summary>
        static void Exercise2()
        {
            List<Product> products = DataLoader.LoadProducts();
            var query = products.Where(p => p.UnitsInStock > 0 && p.UnitPrice > 3);

            PrintProductInformation(query);
        }

        /// <summary>
        /// Print all customer and their order information for the Washington (WA) region.
        /// </summary>
        static void Exercise3()
        {
            List<Customer> customers = DataLoader.LoadCustomers();
            var query = customers.Where(c => c.Region == "WA");

            PrintCustomerInformation(query);
        }

        /// <summary>
        /// Create and print an anonymous type with just the ProductName
        /// </summary>
        static void Exercise4()
        {
            List<Product> products = DataLoader.LoadProducts();
            string[] titles = { "ProductName" };
            string format = Utilities.GenerateProductFormat(titles, false);

            var query = from item in products
                        orderby item.ProductName
                        select new
                        {
                            Name = item.ProductName
                        };

            Utilities.GenerateProductHeaders(titles);

            foreach (var line in query) Console.WriteLine(format, line.Name);
        }

        /// <summary>
        /// Create and print an anonymous type of all product information but increase the unit price by 25%
        /// </summary>
        static void Exercise5()
        {
            List<Product> products = DataLoader.LoadProducts();
            string[] titles = { "ProductID", "ProductName", "Category", "UnitPrice", "UnitsInStock" };
            string format = Utilities.GenerateProductFormat(titles, false);

            var query = from item in products
                        select new
                        {
                            Id = item.ProductID,
                            Name = item.ProductName,
                            Cat = item.Category,
                            Price = item.UnitPrice * 1.25M,
                            Stock = item.UnitsInStock
                        };

            Utilities.GenerateProductHeaders(titles);

            foreach (var line in query)
            {
                Console.WriteLine(format, line.Id, line.Name, line.Cat, line.Price, line.Stock);
            }
        }

        /// <summary>
        /// Create and print an anonymous type of only ProductName and Category with all the letters in upper case
        /// </summary>
        static void Exercise6()
        {
            List<Product> products = DataLoader.LoadProducts();
            string[] titles = { "ProductName", "Category" };
            string format = Utilities.GenerateProductFormat(titles, false);

            var query = products
                .OrderBy(p => p.Category)
                .Select(p => new
                    {
                        Name = p.ProductName.ToUpper(),
                        Cat = p.Category.ToUpper()
                    }
                );

            Utilities.GenerateProductHeaders(titles);

            foreach (var line in query)
            {
                Console.WriteLine(format, line.Name, line.Cat);
            }
        }

        /// <summary>
        /// Create and print an anonymous type of all Product information with an extra bool property ReOrder which should 
        /// be set to true if the Units in Stock is less than 3
        /// </summary>
        static void Exercise7()
        {
            List<Product> products = DataLoader.LoadProducts();
            string format = "{0,-5} {1,-35} {2,-15} {3,8:c} {4,6} {5,9}";

            var query = products.Select(p => new
                {
                    Id = p.ProductID,
                    Name = p.ProductName,
                    Cat = p.Category,
                    Price = p.UnitPrice,
                    Stock = p.UnitsInStock,
                    Reorder = p.UnitsInStock < 3
                }
            );

            Console.WriteLine(format, "ID", "Product Name", "Category", "Unit", "Stock", "ReOrder");
            Console.Write(Utilities.separator);

            foreach (var line in query)
            {
                Console.WriteLine(format, line.Id, line.Name, line.Cat, line.Price, line.Stock,
                    line.Reorder);
            }
        }

        /// <summary>
        /// Create and print an anonymous type of all Product information with an extra decimal called 
        /// StockValue which should be the product of unit price and units in stock
        /// </summary>
        static void Exercise8()
        {
            List<Product> products = DataLoader.LoadProducts();
            string[] titles = { "ProductID", "ProductName", "Category", "UnitPrice", "UnitsInStock", "Value" };
            string format = Utilities.GenerateProductFormat(titles, false);

            var query = products.Select(p => new
                {
                    Id = p.ProductID,
                    Name = p.ProductName,
                    Cat = p.Category,
                    Price = p.UnitPrice,
                    Stock = p.UnitsInStock,
                    StockVal = p.UnitPrice * p.UnitsInStock
                }
            ).OrderByDescending(p => p.StockVal);

            Utilities.GenerateProductHeaders(titles);

            foreach (var line in query)
            {
                Console.WriteLine(format, line.Id, line.Name, line.Cat, line.Price, line.Stock,
                    line.StockVal);
            }
        }

        /// <summary>
        /// Print only the even numbers in NumbersA
        /// </summary>
        static void Exercise9()
        {
            var query = DataLoader.NumbersA
                .Where(n => n % 2 == 0)
                .Select(n => n);

            Console.WriteLine("Even numbers in \"Numbers A array\"");
            Console.Write(Utilities.separator);

            foreach (var number in query) Console.WriteLine(number);
        }

        /// <summary>
        /// Print only customers that have an order whose total is less than $500
        /// </summary>
        static void Exercise10()
        {
            List<Customer> customers = DataLoader.LoadCustomers();
            string[] titles = { "CompanyName", "UnitsInStock" };
            string format = Utilities.GenerateProductFormat(titles, false);

            var query = from client in customers
                        from order in client.Orders
                        where order.Total < 500.00M
                        group order by client.CompanyName into companies
                        select new
                        {
                            Company = companies.Key,
                            Count = companies.Count()
                        };

            Utilities.GenerateProductHeaders(titles);

            foreach (var line in query)
            {
                Console.WriteLine(format, line.Company, line.Count);
            }
        }

        /// <summary>
        /// Print only the first 3 odd numbers from NumbersC
        /// </summary>
        static void Exercise11()
        {
            var query = DataLoader.NumbersC
                .Where(n => n % 2 == 1)
                .Select(n => n)
                .Take(3);

            Console.WriteLine("3 first odd numbers in \"Numbers C array\"");
            Console.Write(Utilities.separator);

            foreach (var number in query) Console.WriteLine(number);
        }

        /// <summary>
        /// Print the numbers from NumbersB except the first 3
        /// </summary>
        static void Exercise12()
        {
            var query = DataLoader.NumbersB
                .Select(n => n)
                .Skip(3);

            Console.WriteLine("Skip 3 first numbers in \"Numbers B array\"");
            Console.Write(Utilities.separator);

            foreach (var number in query) Console.WriteLine(number);
        }

        /// <summary>
        /// Print the Company Name and most recent order for each customer in Washington
        /// </summary>
        static void Exercise13()
        {
            List<Customer> customers = DataLoader.LoadCustomers();
            string[] titles = { "CompanyName", "OrderDate" };
            string format = Utilities.GenerateProductFormat(titles, false);

            var query = customers
                .Where(c => c.Region == "WA")
                .Select(c => new
                    {
                        Company = c.CompanyName,
                        Recent = c.Orders
                            .OrderByDescending(o => o.OrderDate)
                            .First()
                            .OrderDate
                            .ToString("yyyy/MM/dd")
                    }
                );

            Utilities.GenerateProductHeaders(titles);

            foreach (var line in query)
            {
                Console.WriteLine(format, line.Company, line.Recent);
            }
        }

        /// <summary>
        /// Print all the numbers in NumbersC until a number is >= 6
        /// </summary>
        static void Exercise14()
        {
            var query = DataLoader.NumbersC.TakeWhile(n => n <= 6);

            Console.WriteLine("Print numbers in \"Numbers B array\" while <= 6");
            Console.WriteLine(Utilities.separator);

            foreach (var number in query) Console.WriteLine(number);
        }

        /// <summary>
        /// Print all the numbers in NumbersC that come after the first number divisible by 3
        /// </summary>
        static void Exercise15()
        {
            var query = DataLoader.NumbersC
                .SkipWhile(n => n % 3 != 0)
                .Skip(1);

            Console.WriteLine("Numbers in \"Numbers C array\" after first divisible by 3");
            Console.Write(Utilities.separator);

            foreach (var number in query) Console.WriteLine(number);
        }

        /// <summary>
        /// Print the products alphabetically by name
        /// </summary>
        static void Exercise16()
        {
            List<Product> products = DataLoader.LoadProducts();
            string[] titles = { "ProductName" };
            string format = Utilities.GenerateProductFormat(titles, false);

            var query = products
                .OrderBy(p => p.ProductName)
                .Select(p => p.ProductName);

            Utilities.GenerateProductHeaders(titles);

            foreach (var line in query) Console.WriteLine(format, line);
        }

        /// <summary>
        /// Print the products in descending order by units in stock
        /// </summary>
        static void Exercise17()
        {
            List<Product> products = DataLoader.LoadProducts();

            var query = products
                .OrderByDescending(p => p.UnitsInStock);

            PrintProductInformation(query);
        }

        /// <summary>
        /// Print the list of products ordered first by category, then by unit price, from highest to lowest.
        /// </summary>
        static void Exercise18()
        {
            List<Product> products = DataLoader.LoadProducts();

            var query = products
                .OrderBy(p => p.Category)
                .ThenByDescending(p => p.UnitPrice);

            PrintProductInformation(query);
        }

        /// <summary>
        /// Print NumbersB in reverse order
        /// </summary>
        static void Exercise19()
        {
            var query = DataLoader.NumbersB.Reverse();

            foreach (var number in query) Console.WriteLine(number);
        }

        /// <summary>
        /// Group products by category, then print each category name and its products
        /// ex:
        /// 
        /// Beverages
        /// Tea
        /// Coffee
        /// 
        /// Sandwiches
        /// Turkey
        /// Ham
        /// </summary>
        static void Exercise20()
        {
            List<Product> products = DataLoader.LoadProducts();

            var query = products
                .OrderBy(p => p.Category)
                .GroupBy(p => p.Category);

            foreach (var category in query)
            {
                Console.Write(Utilities.separator);
                Console.WriteLine(category.Key);
                Console.Write(Utilities.separator);

                foreach (var product in category)
                {
                    Console.WriteLine($"{product.ProductName}");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Print all Customers with their orders by Year then Month
        /// ex:
        /// 
        /// Joe's Diner
        /// 2015
        ///     1 -  $500.00
        ///     3 -  $750.00
        /// 2016
        ///     2 - $1000.00
        /// </summary>
        static void Exercise21()
        {
            List<Customer> customers = DataLoader.LoadCustomers();

            var query = from c in customers
                select new
                {
                    Company = c.CompanyName,
                    OrderByYears =
                    (
                        from o in c.Orders
                        group o by o.OrderDate
                        into orderYear
                        select new
                        {
                            Year = orderYear.Key,
                            OrderByMonths =
                            (
                                from m in orderYear
                                group m by m.OrderDate.Month into orderMonth
                                select new
                                {
                                    Month = orderMonth.Key,
                                    Orders = orderMonth
                                }
                            )
                        }
                    )
                };
            foreach (var customer in query)
            {
                Console.WriteLine();
                Console.WriteLine($"Customer: {customer.Company}");

                foreach (var year in customer.OrderByYears)
                {
                    Console.WriteLine($"\n\tYear: {year.Year}");

                    foreach (var month in year.OrderByMonths)
                    {
                        Console.WriteLine($"\t\tMonth: {month.Month}");

                        foreach (var order in month.Orders)
                        {
                            Console.WriteLine($"\t\t\tOrder: ${order.Total}");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Print the unique list of product categories
        /// </summary>
        static void Exercise22()
        {
            List<Product> products = DataLoader.LoadProducts();
            string[] titles = { "Category" };
            string format = Utilities.GenerateProductFormat(titles, false);

            var query = products
                .OrderBy(p => p.Category)
                .GroupBy(p => p.Category);

            Utilities.GenerateProductHeaders(titles);
            foreach (var category in query) Console.WriteLine(category.Key);
        }

        /// <summary>
        /// Write code to check to see if Product 789 exists
        /// </summary>
        static void Exercise23()
        {
            List<Product> products = DataLoader.LoadProducts();

            const int id = 789;
            var query = products.FirstOrDefault(p => p.ProductID == id);

            if (query != null)
                Console.WriteLine($"Product #{id} exists in the system. It is {query.ProductName}.");
            else
                Console.WriteLine($"We could not locate product #{id} in the system.");
        }

        /// <summary>
        /// Print a list of categories that have at least one product out of stock
        /// </summary>
        static void Exercise24()
        {
            List<Product> products = DataLoader.LoadProducts();
            string[] titles = { "Category" };
            string format = Utilities.GenerateProductFormat(titles, false);

            var query = products
                .Where(p => p.UnitsInStock == 0)
                .OrderBy(p => p.Category)
                .GroupBy(p => p.Category)
                .Select(c => c.Key);

            Utilities.GenerateProductHeaders(titles);
            foreach (var category in query) Console.WriteLine(format, category);
        }

        /// <summary>
        /// Print a list of categories that have no products out of stock
        /// </summary>
        static void Exercise25()
        {
            List<Product> products = DataLoader.LoadProducts();
            string[] titles = { "Category" };
            string format = Utilities.GenerateProductFormat(titles, false);

            var query = products
                .OrderBy(p => p.Category)
                .GroupBy(p => p.Category)
                .Where(g => g.All(p => p.UnitsInStock > 0))
                .Select(g => g.Key);

            Utilities.GenerateProductHeaders(titles);
            foreach (var category in query) Console.WriteLine(format, category);
        }

        /// <summary>
        /// Count the number of odd numbers in NumbersA
        /// </summary>
        static void Exercise26()
        {
            var query = DataLoader.NumbersA.Count(n => n % 2 == 1);

            Console.WriteLine($"The number of odd numbers in the NumbersA list is {query}.");
        }

        /// <summary>
        /// Create and print an anonymous type containing CustomerId and the count of their orders
        /// </summary>
        static void Exercise27()
        {
            List<Customer> customers = DataLoader.LoadCustomers();
            string format = "{0,-10} {1,-6}";

            var query = customers.Select(c => new
                {
                    Id = c.CustomerID,
                    OrderCount = c.Orders.Length
                }
            );

            Console.Write(Utilities.separator);
            Console.WriteLine(format, "ID", "# of Orders");
            Console.Write(Utilities.separator);

            foreach (var customer in query)
            {
                Console.WriteLine(format, customer.Id, customer.OrderCount);
            }
        }

        /// <summary>
        /// Print a distinct list of product categories and the count of the products they contain
        /// </summary>
        static void Exercise28()
        {
            List<Product> products = DataLoader.LoadProducts();
            string[] titles = { "Category", "UnitsInStock" };
            string format = Utilities.GenerateProductFormat(titles, false);

            var query = products
                .OrderBy(p => p.Category)
                .GroupBy(p => p.Category)
                .Select(g => new
                    {
                        Type = g.Key,
                        Units = g.Count()
                    }
                );

            Utilities.GenerateProductHeaders(titles);

            foreach (var category in query)
            {
                Console.WriteLine(format, category.Type, category.Units);
            }
        }

        /// <summary>
        /// Print a distinct list of product categories and the total units in stock
        /// </summary>
        static void Exercise29()
        {
            List<Product> products = DataLoader.LoadProducts();
            string[] titles = { "Category", "UnitsInStock" };
            string format = Utilities.GenerateProductFormat(titles, false);

            var query = products
                .OrderBy(p => p.Category)
                .GroupBy(p => p.Category)
                .Select(g => new
                    {
                        Type = g.Key,
                        Units = g.Sum(p => p.UnitsInStock)
                    }
                );

            Utilities.GenerateProductHeaders(titles);

            foreach (var category in query)
            {
                Console.WriteLine(format, category.Type, category.Units);
            }
        }

        /// <summary>
        /// Print a distinct list of product categories and the lowest priced product in that category
        /// </summary>
        static void Exercise30()
        {
            List<Product> products = DataLoader.LoadProducts();
            string[] titles = { "Category", "UnitPrice" };
            string format = Utilities.GenerateProductFormat(titles, false);

            var query = products
                .OrderBy(p => p.Category)
                .GroupBy(p => p.Category)
                .Select(g => new
                    {
                        Type = g.Key,
                        MinPrice = g.Min(p => p.UnitPrice)
                    }
                );

            Utilities.GenerateProductHeaders(titles);

            foreach (var category in query)
            {
                Console.WriteLine(format, category.Type, category.MinPrice);
            }
        }

        /// <summary>
        /// Print the top 3 categories by the average unit price of their products
        /// </summary>
        static void Exercise31()
        {
            List<Product> products = DataLoader.LoadProducts();
            string[] titles = { "Category", "UnitPrice" };
            string format = Utilities.GenerateProductFormat(titles, false);

            var query = products
                .OrderBy(p => p.Category)
                .GroupBy(p => p.Category)
                .Select(g => new
                    {
                        Type = g.Key,
                        AvgPrice = g.Average(p => p.UnitPrice)
                    }
                )
                .OrderByDescending(g => g.AvgPrice)
                .Take(3);

            Utilities.GenerateProductHeaders(titles);

            foreach (var category in query)
            {
                Console.WriteLine(format, category.Type, category.AvgPrice);
            }
        }
    }
}
