namespace RPS.BLL
{
    public class Outcome
    {
        public Choice Winner;
        public Choice Loser;
        public string Action;

        public Outcome(Choice winner, string action, Choice loser)
        {
            Winner = winner;
            Loser = loser;
            Action = action;
        }

        public override string ToString()
        {
            return $"{Winner} {Action} {Loser}.";
        }
    }
}
