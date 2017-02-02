using GuessingGame.BLL;

namespace GuessingGame.UI
{
    public class GameFlow
    {
        GameManager _manager;

        public void PlayGame()
        {
            _manager = new GameManager();
            _manager.Start();
            ConsoleOutput.DisplayTitle();

            GuessResult result;
            int guess;

            do
            {
                guess = ConsoleInput.GetGuessFromUser();
                result = _manager.ProcessGuess(guess);
                ConsoleOutput.DisplayGuessMessage(result);
            } while (result != GuessResult.Victory);
        }
    }
}
