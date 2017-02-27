using System;
using FlooringBusiness.BLL.Workflows;
using FlooringBusiness.Models.Enums;
using FlooringBusiness.Models.Interfaces;

namespace FlooringBusiness.BLL.Factories
{
    public class WorkflowFactory
    {
        public static IWorkflow Create(WorkflowType userInput)
        {
            switch (userInput)
            {
                case WorkflowType.Find:
                    return new FindWorkflow();

                case WorkflowType.Display:
                    return new DisplayWorkflow();

                case WorkflowType.Add:
                    return new AddWorkflow();

                case WorkflowType.Edit:
                    return new EditWorkflow();

                case WorkflowType.Remove:
                    return new RemoveWorkflow();

                case WorkflowType.Quit:
                    return null;

                default:
                    throw new Exception("Workflow type is not a valid choice.");
            }
        }
    }
}
