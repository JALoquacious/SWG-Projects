using FlooringBusiness.Models.Prototypes;

namespace FlooringBusiness.Models.Interfaces
{
    public interface IWorkflow
    {
        Response Execute(Request request);
    }
}
