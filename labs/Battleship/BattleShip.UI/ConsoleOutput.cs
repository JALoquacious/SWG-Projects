using System;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;
using BattleShip.BLL.Ships;

namespace BattleShip.UI
{
    class ConsoleOutput
    {
        public static void DisplayTitle()
        {
            Console.Clear();
            Console.WriteLine(
@"__________    ___________________________.____     ___________ _________ ___ ___ ._____________ 
\______   \  /  _  \__    ___/\__    ___/|    |    \_   _____//   _____//   |   \|   \______   \
 |    |  _/ /  /_\  \|    |     |    |   |    |     |    __)_ \_____  \/    ~    \   ||     ___/
 |    |   \/    |    \    |     |    |   |    |___  |        \/        \    Y    /   ||    |    
 |______  /\____|__  /____|     |____|   |_______ \/_______  /_______  /\___|_  /|___||____|    
        \/         \/                            \/        \/        \/       \/                ");
        }

        public static void DisplayIntro()
        {
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine("Welcome to Battleship, a game of high seas adventure for 2 players!");
            Console.WriteLine("Press <Enter> to continue.");
            Console.WriteLine("-------------------------------------------------------------------");
            Console.ReadLine();
        }

        public static void DisplayPlacementMessage(User player, ShipType ship, int length)
        {
            Console.WriteLine($"\n{player.Name}, place your {ship} on the board.");
            Console.WriteLine($"Your {ship} takes up {length} spots, so plan carefully.");
        }

        public static void DisplayShipResponse(PlaceShipRequest request, ShipPlacement response)
        {
            switch (response)
            {
                case ShipPlacement.NotEnoughSpace:
                    Console.WriteLine("\nThere's not enough space for that ship to be placed there.");
                    break;
                case ShipPlacement.Overlap:
                    Console.WriteLine("\nYour ship overlaps another that is already placed.");
                    break;
                default:
                    Console.WriteLine($"\nYour {request.ShipType} has been successfully placed.");
                    break;
            }
        }

        public static void DisplayBoard(User user, bool isOpponent)
        {
            string[] xAxisLabels =
            {
                "\n      1", "   2", "   3", "   4", "   5", "   6", "   7", "   8", "   9", "   10"
            };
            string[] yAxisLabels = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J"};
            string symbol = " ";

            for (int row = 0; row < 11; row++)
            {
                if (row > 0)
                {
                    Console.WriteLine("\n    -----------------------------------------");
                    Console.Write(yAxisLabels[row - 1]);
                }
                for (int col = 0; col < 11; col++)
                {
                    if (row == 0 && col < 10) Console.Write(xAxisLabels[col]);
                    else
                    {
                        if (row > 0 && row < 11)
                        {
                            Coordinate coord = new Coordinate(row, col);

                            symbol = (isOpponent)
                                ? Utilities.GetShotHistory(user, coord)
                                : Utilities.GetShipHistory(user, coord);

                            Console.Write($" {symbol} ");
                            Console.ResetColor();
                            Console.Write("|");
                        }
                    }
                }
            }
            Console.WriteLine("\n    =========================================");
        }

        public static bool IsValidShot(FireShotResponse response)
        {
            if (response.ShotStatus == ShotStatus.Invalid)
            {
                Console.WriteLine("\nYou cannot shoot beyond the board boundaries.");
                Console.WriteLine("Calibrate your Aegis targeting system and try again.");
                ConsoleInput.Continue();
                return false;
            }
            if (response.ShotStatus == ShotStatus.Duplicate)
            {
                Console.WriteLine("\nYou already shot at that target, chief.");
                Console.WriteLine("The admiral won't let you waste ammunition.");
                ConsoleInput.Continue();
                return false;
            }
            return true;
        }

        public static bool OpponentFleetSunk(FireShotResponse response, User player)
        {
            switch (response.ShotStatus)
            {
                case ShotStatus.Victory:
                    {
                        Console.WriteLine("\nHit! You sunk the last ship in your enemy's fleet!");
                        Console.WriteLine($"\nVICTORY IS YOURS, {player.Name}!");
                        return true;
                    }
                case ShotStatus.HitAndSunk:
                    {
                        Console.WriteLine($"\nHit! The enemy's {response.ShipImpacted} has sunk!");
                        return false;
                    }
                case ShotStatus.Hit:
                    {
                        Console.Write("\nHit! There are reports of major damage from the enemy front.");
                        return false;
                    }
                case ShotStatus.Miss:
                    {
                        Console.WriteLine("\nMiss! Your precious munitions have splashed into the ocean.");
                        return false;
                    }
                default:
                    return false;
            }
        }
    }
}