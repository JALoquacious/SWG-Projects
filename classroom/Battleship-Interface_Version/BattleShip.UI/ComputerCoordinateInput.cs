using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL.Requests;

namespace BattleShip.UI
{
    class ComputerCoordinateInput : ICoordinateGetter
    {
        private Random _rng = new Random();
        public Coordinate GetCoordinate()
        {
            Console.WriteLine("\nComputer is generating a coordinate...");

            int letter = _rng.Next(1, 11);
            int number = _rng.Next(1, 11);

            return new Coordinate(letter, number);
        }
    }
}
