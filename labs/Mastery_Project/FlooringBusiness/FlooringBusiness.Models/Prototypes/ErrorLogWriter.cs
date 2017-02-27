using System.Configuration;
using System.IO;

namespace FlooringBusiness.Models.Prototypes
{
    public class ErrorLogWriter
    {
        private readonly string _filePath = ConfigurationManager.AppSettings["FilePath"] + "ErrorLog.txt";

        public void Annotate(string message)
        {
            using (StreamWriter stream = new StreamWriter(_filePath, true))
            {
                stream.WriteLine(message);
            }
        }
    }
}
