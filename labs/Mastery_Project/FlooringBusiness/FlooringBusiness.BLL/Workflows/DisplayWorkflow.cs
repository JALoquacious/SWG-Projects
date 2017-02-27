using System;
using System.Collections.Generic;
using FlooringBusiness.BLL.Factories;
using FlooringBusiness.Models.Enums;
using FlooringBusiness.Models.Interfaces;
using FlooringBusiness.Models.Prototypes;

namespace FlooringBusiness.BLL.Workflows
{
    public class DisplayWorkflow : IWorkflow
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

            if (request.Type != WorkflowType.Display)
            {
                throw new Exception($"Expected workflow type: \"{WorkflowType.Display}\"." +
                                    $" Actual workflow type in request: \"{request.Type}\".");
            }

            if (orders == null)
            {
                response.Success = false;
                response.Message = $"No orders exist for {request.Date:d}. Ending workflow...";
                return response;
            }

            response.Success = true;
            response.Message = $"Displaying orders from {request.Date:d}:";
            response.Orders = orders;
            return response;
        }
    }
}
