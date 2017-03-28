using System;
using System.Text.RegularExpressions;
using FlooringBusiness.BLL.Factories;
using FlooringBusiness.BLL.Infrastructure;
using FlooringBusiness.Models.Enums;
using FlooringBusiness.Models.Interfaces;
using FlooringBusiness.Models.Prototypes;

namespace FlooringBusiness.UI.Views
{
    class OrderRemoveView : IView
    {
        public Response MakeRequest(WorkflowType selection)
        {
            string section = "Remove Order(s)";

            Console.Clear();
            Console.Title = $"{Utilities.ProgramTitle} - {section}";
            Utilities.GenerateSeparator('=', Console.WindowWidth, ConsoleColor.Red);
            Console.SetCursorPosition((Console.WindowWidth - section.Length) / 2, Console.CursorTop);
            Console.WriteLine(section.Color("White"));
            Utilities.GenerateSeparator('=', Console.WindowWidth, ConsoleColor.Red);

            var request = new Request
            {
                Type        = WorkflowType.Remove,
                Date        = Utilities.GetDate("Order Date", false),
                OrderNumber = Utilities.GetNumber<int>("Order Number", new Regex(@"^\d{1,5}$"))
            };

            IWorkflow flow = WorkflowFactory.Create(WorkflowType.Find);
            Response response = flow.Execute(request);

            if (response.Success)
            {
                Order removalTarget = response.Order;

                Utilities.PrintOrderDetails(removalTarget, WorkflowType.Remove);
                Console.WriteLine("Please confirm that you want to remove this order.");

                string polarOption = Utilities.GetString("(Y)es or (N)o",
                    new Regex(@"^Y(es)?$|^N(o)?$", RegexOptions.IgnoreCase), false).ToUpper();

                if (polarOption == "Y" || polarOption == "YES")
                {
                    flow = WorkflowFactory.Create(selection);
                    response = flow.Execute(request);
                }
                else
                {
                    response.Success = false;
                    response.Message = $"Order #{removalTarget.OrderNumber} removal cancelled. Ending workflow...";
                    return response;
                }
            }
            return response;
        }

        public void DisplayFeedback(Response response)
        {
            Console.WriteLine($"\n{response.Message}\n");
        }
    }
}
