using System;
using FlooringBusiness.Models.Enums;
using FlooringBusiness.Models.Interfaces;
using FlooringBusiness.UI.Views;

namespace FlooringBusiness.UI.Factories
{
    public class ViewFactory
    {
        public static IView Create(WorkflowType userInput)
        {
            switch (userInput)
            {
                case WorkflowType.Display:
                    return new OrderDisplayView();

                case WorkflowType.Add:
                    return new OrderAddView();

                case WorkflowType.Edit:
                    return new OrderEditView();

                case WorkflowType.Remove:
                    return new OrderRemoveView();

                case WorkflowType.Quit:
                    return null;

                default:
                    throw new Exception("Workflow type is not a valid choice.");
            }
        }
    }
}
