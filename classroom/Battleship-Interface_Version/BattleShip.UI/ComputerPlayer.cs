using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;
using BattleShip.BLL.Ships;

namespace BattleShip.UI
{
    public class ComputerPlayer : IPlayer
    {
        public string Name { get; set; }
        public Board Board { get; set; }

        public ComputerPlayer(string name = "Computer Player")
        {
            Name = name;
            Board = new Board();
        }

        public PlaceShipRequest GetPlaceShipRequest(ShipType vessel)
        {
            return new PlaceShipRequest()
            {
                Coordinate = Utilities.GenerateCoordinate(),
                Direction = Utilities.GenerateShipDirection(),
                ShipType = vessel
            };
        }
        public void SetUpBoard()
        {
            var ships = new Dictionary<ShipType, int>()
            {
                { ShipType.Destroyer,  2 },
                { ShipType.Cruiser,    3 },
                { ShipType.Submarine,  3 },
                { ShipType.Battleship, 4 },
                { ShipType.Carrier,    5 }
            };

            foreach (KeyValuePair<ShipType, int> vessel in ships)
            {
                ShipPlacement response;
                do
                {
                    PlaceShipRequest request = GetPlaceShipRequest(vessel.Key);
                    response = Board.PlaceShip(request);

                } while (response != ShipPlacement.Ok);
            }
        }
    }
}