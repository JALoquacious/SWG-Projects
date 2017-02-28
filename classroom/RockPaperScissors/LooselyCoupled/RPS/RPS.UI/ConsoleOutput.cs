using System;
using RPS.BLL;

namespace RPS.UI
{
    public class ConsoleOutput
    {
        public static void DisplayResult(Choice playerChoice, Choice computerChoice, Determination outcome)
        {
            Console.WriteLine($"You picked {playerChoice}; Computer picked {computerChoice}.");
            Console.WriteLine(outcome);
        }
    }
}
