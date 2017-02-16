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

        public static Coordinate GenerateCoordinate()
        {
            Console.WriteLine("\nComputer is generating a coordinate...");

            int letter = _generator.Next(1, 11);
            int number = _generator.Next(1, 11);

            return new Coordinate(letter, number);
        }

        public static ShipDirection GenerateShipDirection()
        {
            Console.WriteLine("\nComputer is generating a ship orientation...");

            int direction = _generator.Next(0, 4);

            return (ShipDirection)direction;
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

        public static string GetShipHistory(IPlayer player, Coordinate coord)
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

        public static string GetShotHistory(IPlayer opponent, Coordinate coord)
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

        public static void Retaliate(bool player1Turn, IPlayer player1, IPlayer player2)
        {
            bool gameOver = false;
            IPlayer player = (player1Turn) ? player1 : player2;
            IPlayer opponent = (player == player1) ? player2 : player1;

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
