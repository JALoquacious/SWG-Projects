using System;
using System.Collections.Generic;
using System.Linq;

namespace RPS.BLL
{
    public class Determination
    {
        private bool? _isPlayerWin;
        private Outcome _outcome;

        public static List<Outcome> Actions = new List<Outcome>
        {
            new Outcome(Choice.Scissors, "cut", Choice.Paper),
            new Outcome(Choice.Paper, "covers", Choice.Rock),
            new Outcome(Choice.Rock, "crushes", Choice.Lizard),
            new Outcome(Choice.Lizard, "poisons", Choice.Spock),
            new Outcome(Choice.Spock, "smashes", Choice.Scissors),
            new Outcome(Choice.Scissors, "decapitate", Choice.Lizard),
            new Outcome(Choice.Lizard, "eats", Choice.Paper),
            new Outcome(Choice.Paper, "disproves", Choice.Spock),
            new Outcome(Choice.Spock, "vaporizes", Choice.Rock),
            new Outcome(Choice.Rock, "crushes", Choice.Scissors)
        };

        private Determination(bool? isPlayerWin, Outcome outcome)
        {
            _isPlayerWin = isPlayerWin;
            _outcome = outcome;
        }

        public static Determination DetermineWinner(Choice player, Choice computer)
        {
            var result = FindAction(player, computer);
            if (result != null) return new Determination(true, result);

            result = FindAction(computer, player);
            if (result != null) return new Determination(false, result);

            return new Determination(null, null);
        }

        public static Outcome FindAction(Choice player1, Choice player2)
        {
            return Actions.FirstOrDefault(a => a.Winner == player1 && a.Loser == player2);
        }

        public override string ToString()
        {
            if (_isPlayerWin == false) return $"{_outcome}. You lose!";
            if (_isPlayerWin == true) return $"{_outcome}. You win!";
            return "It's a draw!";
        }
    }
}
