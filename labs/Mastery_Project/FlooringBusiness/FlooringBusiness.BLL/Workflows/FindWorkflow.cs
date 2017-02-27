using System.Collections.Generic;
using System.Linq;
using FlooringBusiness.BLL.Factories;
using FlooringBusiness.Models.Interfaces;
using FlooringBusiness.Models.Prototypes;

namespace FlooringBusiness.BLL.Workflows
{
    public class FindWorkflow : IWorkflow
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

            if (orders == null)
            {
                response.Success = false;
                response.Message = $"No orders exist for {request.Date:d}. Ending workflow...";
                return response;
            }

            Order target = orders.FirstOrDefault(o => o.OrderNumber == request.OrderNumber);

            if (target != null)
            {
                response.Order = target;
                response.Success = true;
                response.Message = "Order(s) matching specification found. Returning to Main Menu...";
                return response;
            }

            response.Success = false;
            response.Message = "Order number not found in database. Ending workflow...";
            return response;
        }
    }
}
