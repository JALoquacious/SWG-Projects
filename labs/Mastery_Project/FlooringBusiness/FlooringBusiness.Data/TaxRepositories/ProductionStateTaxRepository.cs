using System;
using FlooringBusiness.Models.Interfaces;
using FlooringBusiness.Models.Prototypes;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace FlooringBusiness.Data.TaxRepositories
{
    public class ProductionStateTaxRepository : IStateTaxRepository
    {
        private readonly ErrorLogWriter _errorLog = new ErrorLogWriter();
        private readonly string _filePath = ConfigurationManager.AppSettings["FilePath"] + "Taxes.txt";

        public StateTax GetStateTax(string targetState)
        {
            StateTax tax = new StateTax();
            Regex pattern = new Regex(targetState + @"\,[A-Za-z]+,\d+(\.\d+)?", RegexOptions.IgnoreCase);

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
                            tax.StateAbbreviation = columns[0].ToUpper();
                            tax.StateName = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(columns[1].ToLower());
                            tax.TaxRate = decimal.Parse(columns[2]);

                            return tax;
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
    }
}