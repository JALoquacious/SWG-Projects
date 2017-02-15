using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL.Requests;

namespace BattleShip.UI
{
    class HumanDirectionInput : IDirectionGetter
    {
        public ShipDirection GetDirection()
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
    }
}
