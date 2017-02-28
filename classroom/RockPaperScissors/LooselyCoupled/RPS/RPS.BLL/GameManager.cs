namespace RPS.BLL
{
    public class GameManager
    {
        private IChoiceGetter _chooser;

        public GameManager(IChoiceGetter concrete)
        {
            _chooser = concrete;
        }

        public Choice GetComputerChoice()
        {
            return _chooser.GetChoice();
        }

        public Determination PlayRound(Choice playerChoice, Choice computerChoice)
        {
            return Determination.DetermineWinner(playerChoice, computerChoice);
        }
    }
}
