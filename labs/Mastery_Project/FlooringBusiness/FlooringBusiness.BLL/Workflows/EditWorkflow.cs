using System;
using System.Collections.Generic;
using FlooringBusiness.BLL.Factories;
using FlooringBusiness.Models.Enums;
using FlooringBusiness.Models.Interfaces;
using FlooringBusiness.Models.Prototypes;

namespace FlooringBusiness.BLL.Workflows
{
    public class EditWorkflow : IWorkflow
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
            Product product = _orderRepository.LoadProductData(request.Product);
            StateTax stateTax = _orderRepository.LoadStateTaxData(request.State);
            
            if (request.Type != WorkflowType.Edit)
            {
                throw new Exception($"Expected workflow type: \"{WorkflowType.Edit}\"." +
                                    $" Actual workflow type in request: {request.Type}.");
            }

            if (orders == null)
            {
                response.Success = false;
                response.Message = $"No orders exist for {request.Date:d}. Ending workflow...";
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

            Order editedOrder = new Order()
            {
                OrderNumber = request.OrderNumber,
                Customer = request.Customer,
                Area = request.Area,
                Product = product,
                StateTax = stateTax,
                Total = (product.CostPerSquareFoot
                                + product.LaborCostPerSquareFoot)
                                * request.Area
                                * (1 + (stateTax.TaxRate / 100))
            };

            orders[request.OrderNumber - 1] = editedOrder;
            _orderRepository.SaveOrders(request.Date, orders);

            return response;
        }
    }
}
