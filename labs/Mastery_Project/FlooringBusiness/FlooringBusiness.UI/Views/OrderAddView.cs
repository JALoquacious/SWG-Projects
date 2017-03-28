using System;
using System.Text.RegularExpressions;
using FlooringBusiness.BLL.Factories;
using FlooringBusiness.BLL.Infrastructure;
using FlooringBusiness.Models.Enums;
using FlooringBusiness.Models.Interfaces;
using FlooringBusiness.Models.Prototypes;

namespace FlooringBusiness.UI.Views
{
    public class OrderAddView : IView
    {
        public Response MakeRequest(WorkflowType selection)
        {
            string section = "Add Order(s)";

            Console.Clear();
            Console.Title = $"{Utilities.ProgramTitle} - {section}";
            Utilities.GenerateSeparator('=', Console.WindowWidth, ConsoleColor.Green);
            Console.SetCursorPosition((Console.WindowWidth - section.Length) / 2, Console.CursorTop);
            Console.WriteLine(section.Color("White"));
            Utilities.GenerateSeparator('=', Console.WindowWidth, ConsoleColor.Green);

            var request = new Request
            {
                Type     = WorkflowType.Add,
                Date     = Utilities.GetDate("Order Date", true),
                Customer = Utilities.GetString("Company Name", new Regex(@"^[\w ,.]{2,20}$"), false).Trim().ToTitle(),
                Product  = Utilities.GetString("Product Name", new Regex(@"^[\w ,.]{2,20}$"), false).Trim().ToTitle(),
                State    = Utilities.GetString("State", new Regex(@"^[A-Za-z]{2}$"), false).ToUpper(),
                Area     = Utilities.GetNumber<decimal>("Area of Material in Sq. Ft. (minimum: 100)", new Regex(@"^\d{3,7}(\.\d+)?$"))
            };

            IWorkflow flow = WorkflowFactory.Create(selection);
            Response response = flow.Execute(request);
            return response;
        }

        public void DisplayFeedback(Response response)
        {
            Console.WriteLine($"\n{response.Message}\n");
        }
    }
}
