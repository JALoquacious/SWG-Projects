using BattleShip.BLL.GameLogic;

namespace BattleShip.UI
{
    public class User
    {
        public string Name { get; set; }
        public Board Board { get; set; }

        public class Player1 : User
        {
            public Player1(string name = "Player 1")
            {
                Name = name;
                Board = new Board();
            }
        }

        public class Player2 : User
        {
            public Player2(string name = "Player 2")
            {
                Name = name;
                Board = new Board();
            }
        }
    }
}
