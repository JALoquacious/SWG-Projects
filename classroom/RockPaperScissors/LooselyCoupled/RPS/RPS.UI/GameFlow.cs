using RPS.BLL;
using System;

namespace RPS.UI
{
    public class GameFlow
    {
        public void Start()
        {
            GameManager gm = new GameManager(new RandomChoice());

            while(true)
            {
                Console.Clear();
                Choice playerChoice = ConsoleInput.GetUserChoice();
                Choice computerChoice = gm.GetComputerChoice();
                Determination outcome = gm.PlayRound(playerChoice, computerChoice);

                ConsoleOutput.DisplayResult(playerChoice, computerChoice, outcome);
                if (!ConsoleInput.QueryPlayAgain())
                    return;
            }
        }
        
    }
}
