using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Ships;

namespace BattleShip.UI
{
    public class User
    {
        public string Name { get; set; }
        public Board Board { get; set; }

        public class Player1 : User, IPlayer
        {
            public Player1(string name = "Player 1")
            {
                Name = name;
                Board = new Board();
            }
            public PlaceShipRequest GetPlaceShipRequest(ShipType ship)
            {
                throw new System.NotImplementedException();
            }
        }

        public class Player2 : User, IPlayer
        {
            public Player2(string name = "Player 2")
            {
                Name = name;
                Board = new Board();
            }
            public PlaceShipRequest GetPlaceShipRequest(ShipType ship)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
