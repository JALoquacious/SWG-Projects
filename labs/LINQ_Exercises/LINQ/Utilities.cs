using System;
using System.Collections.Generic;

namespace LINQ
{
    public class Utilities
    {
        public static string Separator = GenerateSeparator();

        // generate console-width separator
        private static string GenerateSeparator()
        {
            int width = Console.WindowWidth;
            string symbol = "=";
            string line = "";

            for (int i = 0; i < width; i++)
            {
                line += symbol;
            }
            return line;
        }

        private static string AppendCurrency(bool isCurrency)
        {
            return (isCurrency) ? ":c" : "";
        }

        // generate string based on column titles, ex: "{0,-5} {1,10:c} {2,5}"
        public static string GenerateProductFormat(string[] titles, bool isHeader)
        {
            string format = String.Empty;

            Dictionary<string, string> formatLookup = new Dictionary<string, string>()
            {
                // product queries
                ["ProductID"] = "-5",
                ["ProductName"] = "-35",
                ["Category"] = "-15",
                ["UnitPrice"] = "10" + AppendCurrency(!isHeader),
                ["UnitsInStock"] = "5",
                ["Value"] = "12" + AppendCurrency(!isHeader),

                // customer queries
                ["CustomerID"] = "-10",
                ["CompanyName"] = "-35",

                // order queries
                ["OrderID"] = "-5",
                ["OrderDate"] = "10",
                ["Total"] = "10" + AppendCurrency(!isHeader)
            };

            for (int i = 0; i < titles.Length; i++)
            {
                format += $"{{{i},{formatLookup[titles[i]]}}}";

                if (i < titles.Length - 1) // no space added after last item in array
                {
                    format += " ";
                }
            }
            return format;
        }

        // generate formatted header area
        public static void GenerateProductHeaders(string[] titles)
        {
            string[] header = new string[titles.Length];
            string format = GenerateProductFormat(titles, true);

            Dictionary<string, string> headerLookup = new Dictionary<string, string>()
            {
                // product queries
                ["ProductID"] = "ID",
                ["ProductName"] = "Product",
                ["Category"] = "Category",
                ["UnitPrice"] = "/Unit",
                ["UnitsInStock"] = "#",
                ["Value"] = "Value",

                // customer queries
                ["CustomerID"] = "ClientID",
                ["CompanyName"] = "Company",

                // order queries
                ["OrderID"] = "OrderID",
                ["OrderDate"] = "Date",
                ["Total"] = "Total"
            };

            for (int i = 0; i < titles.Length; i++)
            {
                header[i] = headerLookup[titles[i]];
            }

            Console.Write(Separator);
            Console.WriteLine(format, header);
            Console.Write(Separator);
        }

        public static void ProductLoop(string[] titles, IEnumerable<dynamic> query)
        {
            string format = GenerateProductFormat(titles, true);
            string[] props = new string[titles.Length];

            foreach (var item in query)
            {
                // props is array of properties, ex: {item.Category, item.AnonymousProp}
                // ** would need to be able to access anonymous type properties - tricky
                Console.WriteLine(format, props);
            }
            Console.WriteLine();
        }
    }
}