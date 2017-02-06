using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;
using BattleShip.BLL.Ships;

namespace BattleShip.UI
{
    public class Utilities
    {
        private static readonly Random _generator = new Random();
        public static bool FirstTurn()
        {
            return _generator.Next(100) < 50;
        }

        public static bool IsValidYesNo(string input)
        {
            return new Regex(@"^(y(es)?|n(o)?)$", RegexOptions.IgnoreCase).IsMatch(input);
        }

        public static bool IsValidName(string name)
        {
            return new Regex(@"^[a-zA-Z]{1,20}$").IsMatch(name);
        }

        public static bool IsValidCoordinate(string coordinate)
        {
            Regex valid = new Regex(@"^[a-jA-J][0-9]{1,2}$", RegexOptions.IgnoreCase);
            Match sequence = valid.Match(coordinate);
            return sequence.Success;
        }

        public static bool IsValidDirection(string direction)
        {
            Regex valid = new Regex(@"^(u(p)?|d(own)?|l(eft)?|r(ight)?)$", RegexOptions.IgnoreCase);
            Match sequence = valid.Match(direction);
            return sequence.Success;
        }

        public static Coordinate ConvertToCoordinate(string coordinate)
        {
            char prefix = Convert.ToChar(coordinate.Substring(0, 1).ToUpper());
            string suffix = coordinate.Substring(1);
            int row = prefix % 64; // difference between ASCII uppercase letters and numbers
            int col = int.Parse(suffix);
            return new Coordinate(row, col);
        }

        public static void ClearScreenPrompt()
        {
            Console.WriteLine("\nYour turn is over. To hide your data from your opponent,");
            Console.WriteLine("hit <Enter> to clear your screen and proceed to his/her turn.");
            Console.ReadLine();
            Console.Clear();
        }

        public static string GetShipHistory(User player, Coordinate coord)
        {
            Ship[] ships = player.Board.Ships;
            for (int i = 0; i < ships.Length; i++)
            {
                if (ships[i] != null)
                {
                    foreach (var position in ships[i].BoardPositions)
                    {
                        if (position.Equals(coord))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            return "S";
                        }
                    }
                }
            }
            return " ";
        }

        public static string GetShotHistory(User opponent, Coordinate coord)
        {
            if (opponent.Board.ShotHistory.ContainsKey(coord))
            {
                switch (opponent.Board.ShotHistory[coord])
                {
                    case ShotHistory.Hit:
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        return "H";
                    }
                    case ShotHistory.Miss:
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        return "M";
                    }
                }
            }
            return " ";
        }

        public static void SetUpBoard(User player)
        {
            ShipPlacement response;

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
                do
                {
                    Console.Clear();
                    ConsoleOutput.DisplayBoard(player, false);
                    ConsoleOutput.DisplayPlacementMessage(player, vessel.Key, vessel.Value);
                    var request = new PlaceShipRequest()
                    {
                        Coordinate = ConsoleInput.GetCoordinate(),
                        Direction = ConsoleInput.GetShipDirection(),
                        ShipType = vessel.Key
                    };

                    response = player.Board.PlaceShip(request);
                    Console.Clear();
                    ConsoleOutput.DisplayBoard(player, false);
                    ConsoleOutput.DisplayShipResponse(request, response);
                    ConsoleInput.Continue();
                } while (response != ShipPlacement.Ok);
            }
            Console.Clear();
            ConsoleOutput.DisplayBoard(player, false);
            ClearScreenPrompt();
        }

        public static void Retaliate(bool player1Turn, User player1, User player2)
        {
            bool gameOver = false;
            User player = (player1Turn) ? player1 : player2;
            User opponent = (player == player1) ? player2 : player1;

            Console.WriteLine($"{player.Name}, your superior targeting system has given you");
            Console.WriteLine($"first strike advantage. Hit {opponent.Name} before he/she realizes!");
            ConsoleInput.Continue();

            while (!gameOver)
            {
                gameOver = (player1Turn)
                    ? ConsoleInput.Attack(player1, player2)
                    : ConsoleInput.Attack(player2, player1);
                 
                player1Turn = !player1Turn;
            }
        }
    }
}
