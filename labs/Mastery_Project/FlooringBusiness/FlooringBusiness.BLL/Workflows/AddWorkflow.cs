using System;
using System.Collections.Generic;
using System.Linq;
using FlooringBusiness.BLL.Factories;
using FlooringBusiness.Models.Enums;
using FlooringBusiness.Models.Interfaces;
using FlooringBusiness.Models.Prototypes;

namespace FlooringBusiness.BLL.Workflows
{
    public class AddWorkflow : IWorkflow
    {
        private readonly IOrderRepository _orderRepository = OrderRepositoryFactory.Create();

        public int GenerateOrderNumber(List<Order> orders)
        {
            return (orders.Any() ? orders.Max(o => o.OrderNumber) : 0) + 1;
        }

        public Response Execute(Request request)
        {
            Response response = new Response
            {
                Date = request.Date,
                Type = request.Type
            };

            List<Order> orders = _orderRepository.LoadOrders(request.Date) ?? new List<Order>();
            Product product = _orderRepository.LoadProductData(request.Product);
            StateTax stateTax = _orderRepository.LoadStateTaxData(request.State);

            if (request.Type != WorkflowType.Add)
            {
                throw new Exception($"Expected workflow type: \"{WorkflowType.Add}\"." +
                                    $" Actual workflow type in request: {request.Type}.");
            }

            if (request.Date < DateTime.Now)
            {
                response.Success = false;
                response.Message = "Order cannot be added to a past date. Ending workflow...";
                return response;
            }

            if (stateTax == null)
            {
                response.Success = false;
                response.Message = $"State {request.State} not found in database. Ending workflow...";
                return response;
            }

            if (product == null)
            {
                response.Success = false;
                response.Message = $"Product {request.Product} not found in database. Ending workflow...";
                return response;
            }

            Order currentOrder = new Order()
            {
                OrderNumber = GenerateOrderNumber(orders),
                Customer    = request.Customer,
                Area        = request.Area,
                Product     = product,
                StateTax    = stateTax,
                Total       = (product.CostPerSquareFoot
                                + product.LaborCostPerSquareFoot)
                                * request.Area
                                * (1 + (stateTax.TaxRate / 100))
            };

            orders.Add(currentOrder);
            _orderRepository.SaveOrders(request.Date, orders);

            response.Success = true;
            response.Message = "Order added successfully. Returning to Main Menu...";
            return response;
        }
    }
}