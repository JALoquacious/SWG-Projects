using System;
using System.Collections.Generic;
using FlooringBusiness.Models.Prototypes;

namespace FlooringBusiness.Models.Interfaces
{
    public interface IOrderRepository
    {
        List<Order> LoadOrders(DateTime date);
        void SaveOrders(DateTime date, List<Order> orders);
        Product LoadProductData(string productTitle);
        StateTax LoadStateTaxData(string stateAbbreviation);
    }
}
