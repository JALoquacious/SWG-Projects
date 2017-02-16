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
    public class HumanPlayer : IPlayer
    {
        public string Name { get; set; }
        public Board Board { get; set; }

        public HumanPlayer(string name = "Human Player")
        {
            Name = name;
            Board = new Board();
        }

        public PlaceShipRequest GetPlaceShipRequest(ShipType vessel)
        {
            return new PlaceShipRequest()
            {
                Coordinate = ConsoleInput.GetCoordinate(),
                Direction = ConsoleInput.GetShipDirection(),
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
                    Console.Clear();
                    ConsoleOutput.DisplayBoard(this, false);
                    ConsoleOutput.DisplayPlacementMessage(this, vessel.Key, vessel.Value);
                    PlaceShipRequest request = GetPlaceShipRequest(vessel.Key);

                    response = Board.PlaceShip(request);

                    Console.Clear();
                    ConsoleOutput.DisplayBoard(this, false);
                    ConsoleOutput.DisplayShipResponse(request, response);
                    ConsoleInput.Continue();

                } while (response != ShipPlacement.Ok);
            }
            Console.Clear();
            ConsoleOutput.DisplayBoard(this, false);
            ConsoleOutput.ClearScreenPrompt();
        }
    }
}