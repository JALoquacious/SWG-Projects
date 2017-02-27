using System;
using FlooringBusiness.BLL.Infrastructure;
using FlooringBusiness.Models.Enums;
using FlooringBusiness.Models.Interfaces;
using FlooringBusiness.Models.Prototypes;
using FlooringBusiness.UI.Factories;
using FlooringBusiness.UI.Views;

namespace FlooringBusiness.UI
{
    public class ApplicationProcess
    {
        public void Run()
        {
            WorkflowType selection;

            while (true)
            {
                Console.Title = Utilities.ProgramTitle;

                MainMenu.Display();
                selection = MainMenu.GetSelection();

                if (selection == WorkflowType.Quit) break;

                IView view = ViewFactory.Create(selection);
                Response response = view.MakeRequest(selection);
                view.DisplayFeedback(response);

                Utilities.Continue();
            }
        }
    }
}
