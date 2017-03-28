using System;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using FlooringBusiness.Models.Interfaces;
using FlooringBusiness.Models.Prototypes;

namespace FlooringBusiness.Data.ProductRepositories
{
    public class ProductionProductRepository : IProductRepository
    {
        private readonly ErrorLogWriter _errorLog = new ErrorLogWriter();
        private readonly string _filePath = ConfigurationManager.AppSettings["FilePath"] + "Products.txt";

        public Product GetProduct(string targetProduct)
        {
            Product product = new Product();
            Regex pattern = new Regex(targetProduct + @"\,\d+(\.\d+)?,\d+(\.\d+)?", RegexOptions.IgnoreCase);

            try
            {
                using (StreamReader stream = new StreamReader(_filePath))
                {
                    while (!stream.EndOfStream)
                    {
                        string line = stream.ReadLine();
                        string[] columns = line.Split(',');

                        if (line.StartsWith("State")) continue;

                        if (pattern.IsMatch(line))
                        {
                            product.ProductType            = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(columns[0].ToLower());
                            product.CostPerSquareFoot      = decimal.Parse(columns[1]);
                            product.LaborCostPerSquareFoot = decimal.Parse(columns[2]);

                            return product;
                        }
                    }
                }
                return null;
            }
            catch (IOException error)
            {
                _errorLog.Annotate($"{DateTime.Now} -- File I/O Error: {error.Message}");
                return null;
            }
            catch (Exception error)
            {
                _errorLog.Annotate($"{DateTime.Now} -- Unknown Error: {error.Message}");
                return null;
            }
        }
    }
}
