using System;
using System.Text.RegularExpressions;
using FlooringBusiness.BLL.Factories;
using FlooringBusiness.BLL.Infrastructure;
using FlooringBusiness.Models.Enums;
using FlooringBusiness.Models.Interfaces;
using FlooringBusiness.Models.Prototypes;

namespace FlooringBusiness.UI.Views
{
    class OrderEditView : IView
    {
        public Response MakeRequest(WorkflowType selection)
        {
            string section = "Edit Order(s)";

            Console.Clear();
            Console.Title = $"{Utilities.ProgramTitle} - {section}";
            Utilities.GenerateSeparator('=', Console.WindowWidth, ConsoleColor.Blue);
            Console.SetCursorPosition((Console.WindowWidth - section.Length) / 2, Console.CursorTop);
            Console.WriteLine(section.Color("White"));
            Utilities.GenerateSeparator('=', Console.WindowWidth, ConsoleColor.Blue);

            var request = new Request
            {
                Type = WorkflowType.Find,
                Date = Utilities.GetDate("Order Date", false),
                OrderNumber = Utilities.GetNumber<int>("Order Number", new Regex(@"^\d{1,5}$"))
            };

            IWorkflow flow = WorkflowFactory.Create(request.Type);
            Response response = flow.Execute(request);

            if (response.Success)
            {
                Order editTarget = response.Order;
                Utilities.PrintOrderDetails(editTarget, WorkflowType.Edit);

                string newCustomer = Utilities.GetString("New Company Name", new Regex(@"^$|[\w ,.]{2,20}"), false).Trim().ToTitle(); // ?? editTarget.Customer;
                string newProduct = Utilities.GetString("New Product Name", new Regex(@"^$|[\w ,.]{2,20}"), false).Trim().ToTitle(); // ?? editTarget.Product.ToString();
                string newState = Utilities.GetString("New State", new Regex(@"^$|^[A-Za-z]{2}$"), false).ToUpper(); // ?? editTarget.StateTax.StateAbbreviation).ToUpper();
                string newArea = Utilities.GetString("New Area of Material (sq. ft.)", new Regex(@"^$|^\d+(\.\d+)?$"), false);

                request = new Request
                {
                    Type = WorkflowType.Edit,
                    Date = request.Date,
                    OrderNumber = request.OrderNumber,
                    Customer = string.IsNullOrEmpty(newCustomer) ? editTarget.Customer : newCustomer,
                    Product = string.IsNullOrEmpty(newProduct) ? editTarget.Product.ProductType : newProduct,
                    State = string.IsNullOrEmpty(newState) ? editTarget.StateTax.StateAbbreviation : newState,
                    Area = string.IsNullOrEmpty(newArea) ? editTarget.Area : decimal.Parse(newArea)
                };

                flow = WorkflowFactory.Create(WorkflowType.Find);
                response = flow.Execute(request);

                Order updatedSample = new Order()
                {
                    OrderNumber = request.OrderNumber,
                    Customer = request.Customer,
                    Area = request.Area,
                    Product = new Product()
                    {
                        ProductType = request.Product
                    },
                    StateTax = new StateTax()
                    {
                        StateAbbreviation = request.State
                    },
                    Total = (response.Order.Product.CostPerSquareFoot
                                + response.Order.Product.LaborCostPerSquareFoot)
                                * request.Area
                                * (1 + (response.Order.StateTax.TaxRate / 100))
                };

                Utilities.PrintOrderDetails(updatedSample, WorkflowType.Edit);

                Console.WriteLine("\nPlease confirm to keep these changes.");

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
                    response.Message = $"Order #{editTarget.OrderNumber} editing cancelled. Ending workflow...";
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
