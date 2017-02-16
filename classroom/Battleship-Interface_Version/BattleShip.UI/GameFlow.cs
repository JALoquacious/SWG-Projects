namespace BattleShip.UI
{
    public class GameFlow
    {
        public void Play()
        {
            bool gamePlay = true;

            while (gamePlay)
            {
                bool player1Turn = Utilities.FirstTurn();

                // Display splash screen
                ConsoleOutput.DisplayTitle();
                ConsoleOutput.DisplayIntro();

                // Instantiate players
                IPlayer player1 = ConsoleInput.PlayerFactory("1");
                IPlayer player2 = ConsoleInput.PlayerFactory("2");

                // Assign user names
                player1.Name = ConsoleInput.GetPlayerName(player1);
                player2.Name = ConsoleInput.GetPlayerName(player2);

                // Setup ships
                player1.SetUpBoard();
                player2.SetUpBoard();

                // Start dueling
                Utilities.Retaliate(player1Turn, player1, player2);

                // Continue / end game
                gamePlay = ConsoleInput.IsGameEnded();
            }
        }
    }
}