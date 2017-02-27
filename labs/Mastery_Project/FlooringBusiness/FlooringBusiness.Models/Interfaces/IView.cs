using FlooringBusiness.Models.Enums;
using FlooringBusiness.Models.Prototypes;

namespace FlooringBusiness.Models.Interfaces
{
    public interface IView
    {
        Response MakeRequest(WorkflowType selection);
        void DisplayFeedback(Response response);
    }
}
