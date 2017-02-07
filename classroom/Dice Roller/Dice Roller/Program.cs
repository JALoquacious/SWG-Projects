using System;
using System.Collections.Generic;

namespace DiceRoller
{
    class Program
    {
        private static Random _rng = new Random();

        static void Main(string[] args)
        {
            foreach (var num in GetValues())
            {
                Console.WriteLine(num);
            }
            Console.ReadLine();
        }

        public static Dictionary<byte, byte> GetValues()
        {
            Dictionary<byte, byte> rollCount = new Dictionary<byte, byte>();

            for (byte i = 0; i < 100; i++)
            {
                byte roll = RollDice(_rng);

                if (!rollCount.ContainsKey(roll))
                {
                    rollCount.Add(roll, 1);
                }
                else
                {
                    rollCount[roll]++;
                }
            }
            return rollCount;
        }

        public static byte RollDice(Random _rng)
        {
            byte die1;
            byte die2;

            die1 = (byte)_rng.Next(1, 7);
            die2 = (byte)_rng.Next(1, 7);

            return (byte) (die1 + die2);
        }
    }
}
