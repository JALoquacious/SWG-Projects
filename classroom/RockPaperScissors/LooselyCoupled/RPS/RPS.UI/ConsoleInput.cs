using RPS.BLL;
using System;
using System.Collections.Generic;

namespace RPS.UI
{
    public class ConsoleInput
    {
        private IChoiceGetter _chooser;

        public Choice GetComputerChoice()
        {
            return _chooser.GetChoice();
        }

        public static Choice GetUserChoice()
        {
            Dictionary<string, Choice> map = new Dictionary<string, Choice>()
            {
                ["R"] = Choice.Rock,
                ["P"] = Choice.Paper,
                ["S"] = Choice.Scissors,
                ["L"] = Choice.Lizard,
                ["K"] = Choice.Spock
            };

            while(true)
            {
                Console.Clear();
                Console.WriteLine("You're playing a quick game of RPSLK with Sheldon Cooper.\n");
                Console.Write("Enter your choice (R)ock, (P)aper, (S)cissors, (L)izard, or Spoc(K): ");
                string input = Console.ReadLine();
                Choice selection;

                if (map.TryGetValue(input.ToUpper(), out selection)) return selection;

                Console.WriteLine("\nThat was not a valid choice! Try R, P, S, L, or K!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        public static bool QueryPlayAgain()
        {
            while (true)
            {
                Console.Write("\nPlay again? (Y/N): ");
                string input = Console.ReadLine();

                switch (input.ToUpper())
                {
                    case "Y":
                        return true;
                    case "N":
                        return false;
                }
                Console.WriteLine("Please enter Y or N.");
            }
        }
    }
}
