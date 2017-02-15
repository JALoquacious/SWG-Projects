using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL.Requests;

namespace BattleShip.UI
{
    class HumanCoordinateInput : ICoordinateGetter
    {
        public Coordinate GetCoordinate()
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
    }
}
