using System;
using System.Text.RegularExpressions;
using FlooringBusiness.BLL.Infrastructure;
using FlooringBusiness.Models.Enums;

namespace FlooringBusiness.UI.Views
{
    public static class MainMenu
    {
        public static WorkflowType GetSelection()
        {
            Regex spec = new Regex(@"^(\s+)?[1-5](\s+)?$|^(\s+)?Q(uit)?(\s+)?$", RegexOptions.IgnoreCase);

            string input = Utilities.GetString("Menu Choice", spec, false);

            string digit = (input.Trim().ToUpper().StartsWith("Q")) ? "5" : input;

            return (WorkflowType) int.Parse(digit);
        }

        public static void Display()
        {
            Console.Clear();
            Utilities.Indent("====================================", Console.WindowWidth / 2);
            Utilities.Indent($"| {Utilities.ProgramTitle} |", Console.WindowWidth / 2);
            Utilities.Indent("|----------------------------------|", Console.WindowWidth / 2);
            Utilities.Indent("|                                  |", Console.WindowWidth / 2);
            Utilities.Indent("| 1. Display Orders                |", Console.WindowWidth / 2);
            Utilities.Indent("| 2. Add Order                     |", Console.WindowWidth / 2);
            Utilities.Indent("| 3. Edit Order                    |", Console.WindowWidth / 2);
            Utilities.Indent("| 4. Remove Order                  |", Console.WindowWidth / 2);
            Utilities.Indent("| Q. Quit                          |", Console.WindowWidth / 2);
            Utilities.Indent("|                                  |", Console.WindowWidth / 2);
            Utilities.Indent("====================================", Console.WindowWidth / 2);
        }
    }
}
