using System;
using System.Collections.Generic;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;

namespace BattleShip.UI
{
    public class ConsoleInput
    {
        public static void Continue()
        {
            Console.WriteLine("\nPress <Enter> to continue...");
            Console.ReadLine();
        }

        public static string GetPlayerName(User current)
        {
            Console.Clear();
            do
            {
                Console.WriteLine($"{current.Name}, please enter your name: ");
                Console.WriteLine($"Or hit <Enter> without typing anything to use the default name, \"{current.Name}\".");
                string newName = Console.ReadLine();

                if (String.IsNullOrEmpty(newName)) return current.Name;
                if (Utilities.IsValidName(newName))
                {
                    return newName.Substring(0,1).ToUpper() + newName.Substring(1).ToLower();
                }

                Console.WriteLine("That was not a valid input.");

            } while (true);
        }

        public static Coordinate GetCoordinate()
        {
            do
            {
                Console.Write("\nEnter a coordinate <A-J><1-10> (ex: A1, B8, F5, J10): ");
                string input = Console.ReadLine();

                if (Utilities.IsValidCoordinate(input))
                {
                    return Utilities.ConvertToCoordinate(input);
                }

                Console.WriteLine("That was not a valid coordinate.");

            } while (true);
        }

        public static bool IsGameEnded()
        {
            Console.WriteLine("\nGAME OVER!");
            do
            {
                Console.WriteLine("Play again? Yes (Y) / No (N)");
                string input = Console.ReadLine();

                if (Utilities.IsValidYesNo(input))
                {
                    return input.Substring(0,1).ToUpper().Equals("Y");
                }

                Console.WriteLine("Invalid input. Please enter Y or N.");
                
            } while (true);
        }

        public static ShipDirection GetShipDirection()
        {
            var charToAngle = new Dictionary<char, ShipDirection>
            {
                ['U'] = ShipDirection.Up,
                ['D'] = ShipDirection.Down,
                ['L'] = ShipDirection.Left,
                ['R'] = ShipDirection.Right
            };

            do
            {
                Console.WriteLine("Indicate the direction that you'd like to place your ship on the board.");
                Console.Write("Up (U), Down (D), Left (L), Right (R): ");
                string direction = Console.ReadLine().ToUpper();

                if (Utilities.IsValidDirection(direction))
                {
                    return charToAngle[direction[0]];
                }

                Console.WriteLine("That was not a valid direction.");
            } while (true);
        }

        public static bool Attack(User player, User opponent)
        {
            bool isVictory;
            bool targetAcquired;
            FireShotResponse response;
            Coordinate radar;

            do
            {
                Console.Clear();
                ConsoleOutput.DisplayBoard(opponent, true);
                Console.WriteLine($"\n{player.Name}, fire munitions at {opponent.Name}!");

                radar = GetCoordinate();
                response = opponent.Board.FireShot(radar);
                targetAcquired = ConsoleOutput.IsValidShot(response);

            } while (!targetAcquired);

            Console.Clear();
            ConsoleOutput.DisplayBoard(opponent, true);
            isVictory = ConsoleOutput.OpponentFleetSunk(response, player);

            if (isVictory)
            {
                Continue();
                Console.Clear();
                ConsoleOutput.DisplayBoard(opponent, true);
                return true;
            }

            Continue();
            return false;
        }
    }
}