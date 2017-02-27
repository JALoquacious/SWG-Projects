using System;
using FlooringBusiness.Models.Prototypes;

namespace FlooringBusiness.Models.Interfaces
{
    public interface IProductRepository
    {
        Product GetProduct(String title);
    }
}
