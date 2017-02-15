using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL.Requests;

namespace BattleShip.UI
{
    class ComputerDirectionInput : IDirectionGetter
    {
        private Random _rng = new Random();
        public ShipDirection GetDirection()
        {
            Console.WriteLine("\nComputer is generating a ship orientation...");

            int direction = _rng.Next(0, 4);

            return (ShipDirection)direction;
        }
    }
}
