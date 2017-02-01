using System;
using System.Collections.Generic;

namespace GoblinBattle.UI
{
    class Program
    {
        private static Random _rng = new Random();
        static void Main(string[] args)
        {
            Goblin g1 = new Goblin();
            Goblin g2 = new Goblin();
            Array weaponCache = Enum.GetValues(typeof(Weapon));
            Array remedyCache = Enum.GetValues(typeof(Remedy));
            List<Action> move = new List<Action>()
            {
                () => g1.Attack(g2),
                () => g2.Attack(g1),
                () => g1.HasShield = Convert.ToBoolean(_rng.Next(0, 2)),
                () => g2.HasShield = Convert.ToBoolean(_rng.Next(0, 2)),
                () => g1.ChangeWeapon((Weapon)_rng.Next(1, weaponCache.Length)),
                () => g2.ChangeWeapon((Weapon)_rng.Next(1, weaponCache.Length)),
                () => g1.UseRemedy((Remedy)remedyCache.GetValue(_rng.Next(1, remedyCache.Length))),
                () => g2.UseRemedy((Remedy)remedyCache.GetValue(_rng.Next(1, remedyCache.Length)))
            };

            g1.Name = "Unger";
            g1.HitPoints = 1000;

            g2.Name = "Blurg";
            g2.HitPoints = 1000;

            while (!g1.IsDead && !g2.IsDead)
            {
                move[_rng.Next(0, move.Count)]();
            }

            Console.WriteLine("The battle is ended!");
            Console.ReadLine();
        }
    }
}
