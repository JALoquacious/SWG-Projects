using System;
using System.Collections.Generic;
using FlooringBusiness.BLL.Factories;
using FlooringBusiness.Models.Enums;
using FlooringBusiness.Models.Interfaces;
using FlooringBusiness.Models.Prototypes;

namespace FlooringBusiness.BLL.Workflows
{
    public class RemoveWorkflow : IWorkflow
    {
        private readonly IOrderRepository _orderRepository = OrderRepositoryFactory.Create();

        public Response Execute(Request request)
        {
            Response response = new Response
            {
                Date = request.Date,
                Type = request.Type
            };

            List<Order> orders = _orderRepository.LoadOrders(request.Date);

            if (request.Type != WorkflowType.Remove)
            {
                throw new Exception($"Expected workflow type: \"{WorkflowType.Remove}\"." +
                                    $" Actual workflow type in request: {request.Type}.");
            }

            try
            {
                orders.RemoveAll(o => o.OrderNumber == request.OrderNumber);
                _orderRepository.SaveOrders(request.Date, orders);

                response.Orders  = orders;
                response.Success = true;
                response.Message = "Order(s) removed successfully. Returning to Main Menu...";
                return response;
            }
            catch
            {
                response.Success = false;
                response.Message = "An error occurred during attempted order removal. Contact IT for assistance.";
                return response;
            }
        }
    }
}
