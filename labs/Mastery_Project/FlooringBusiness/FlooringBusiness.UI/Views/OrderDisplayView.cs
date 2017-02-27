using System;
using FlooringBusiness.BLL.Factories;
using FlooringBusiness.BLL.Infrastructure;
using FlooringBusiness.Models.Enums;
using FlooringBusiness.Models.Interfaces;
using FlooringBusiness.Models.Prototypes;

namespace FlooringBusiness.UI.Views
{
    public class OrderDisplayView : IView
    {
        public Response MakeRequest(WorkflowType selection)
        {
            string section = "Display Order(s)";

            Console.Clear();
            Console.Title = $"{Utilities.ProgramTitle} - {section}";
            Utilities.GenerateSeparator('=', Console.WindowWidth, ConsoleColor.Yellow);
            Console.SetCursorPosition((Console.WindowWidth - section.Length) / 2, Console.CursorTop);
            Console.WriteLine(section.Color("White"));
            Utilities.GenerateSeparator('=', Console.WindowWidth, ConsoleColor.Yellow);

            var request = new Request
            {
                Type = WorkflowType.Display,
                Date = Utilities.GetDate("Order Date", false)
            };

            IWorkflow flow = WorkflowFactory.Create(selection);
            Response response = flow.Execute(request);
            return response;
        }

        public void DisplayFeedback(Response response)
        {
            Console.Clear();

            if (response.Orders != null)
            {
                Console.WriteLine($"\n{response.Message}\n");

                string format = "{0,-5} {1,-20} {2,-10} {3,-10:0.00} {4,-10} {5,10:0.00} {6,12:c} {7,12:c} {8,12:c}";

                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine(format, "\nID", "Customer", "State", "Tax %", "Type", "Area",
                    "Mat./sf.", "Lab./sf.", "TOTAL");

                Utilities.GenerateSeparator('=', Console.WindowWidth, ConsoleColor.Blue);

                foreach (var order in response.Orders)
                {
                    Console.ForegroundColor = (response.Orders.IndexOf(order) % 2 == 0)
                        ? ConsoleColor.Cyan : ConsoleColor.White;

                    Console.WriteLine(format, order.OrderNumber, order.Customer,
                        order.StateTax.StateAbbreviation, order.StateTax.TaxRate,
                        order.Product.ProductType, order.Area, order.Product.CostPerSquareFoot,
                        order.Product.LaborCostPerSquareFoot, order.Total);

                    Utilities.GenerateSeparator('-', Console.WindowWidth, ConsoleColor.Red);
                }
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
            {
                Console.WriteLine($"\n{response.Message}\n");
            }
        }
    }
}
