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
                User player1 = new User.Player1();
                User player2 = new User.Player2();

                // Assign user names
                player1.Name = ConsoleInput.GetPlayerName(player1);
                player2.Name = ConsoleInput.GetPlayerName(player2);

                // Setup ships
                Utilities.SetUpBoard(player1);
                Utilities.SetUpBoard(player2);

                // Start dueling
                Utilities.Retaliate(player1Turn, player1, player2);

                // Continue / end game
                gamePlay = ConsoleInput.IsGameEnded();
            }
        }
    }
}
