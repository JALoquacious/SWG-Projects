using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringBusiness.Models.Prototypes;

namespace FlooringBusiness.BLL.Infrastructure
{
    public class ExceptionHandler
    {
        private readonly ErrorLogWriter _errorLog = new ErrorLogWriter();

        public void GlobalTryCatch<T>(Func<T> method)
        {
            try
            {
                method.Invoke();
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

        //public void DoSomething()
        //{
        //    GlobalTryCatch(() =>
        //    {
        //        // code here
        //    });
        //}
    }
}
