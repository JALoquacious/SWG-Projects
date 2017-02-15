using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Ships;

namespace BattleShip.UI
{
    public class HumanPlayer :IPlayer
    {
        public string Name { get; set; }
        public Board Board { get; set; }
        public PlaceShipRequest GetPlaceShipRequest(ShipType ship)
        {
            throw new NotImplementedException();
        }
    }
}