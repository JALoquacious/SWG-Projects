using FlooringBusiness.Models.Prototypes;

namespace FlooringBusiness.Models.Interfaces
{
    public interface IStateTaxRepository
    {
        StateTax GetStateTax(string state);
    }
}
