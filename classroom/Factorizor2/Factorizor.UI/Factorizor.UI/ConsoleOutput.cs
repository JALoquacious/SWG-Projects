using System;
using System.Collections.Generic;
using Factorizor.BLL;

namespace Factorizor.UI
{
    public class ConsoleOutput
    {
        public static void DisplayTitle()
        {
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("Welcome to the Factorizer.");
            Console.WriteLine("I'll determine whether a number is perfect and/or prime.");
            Console.WriteLine("If not prime, I'll give you the number's factors.");
            Console.WriteLine("--------------------------------------------------------");
        }

        public static void DisplayFactors(int number, int[] factors)
        {
            if (number == 0)
            {
                Console.WriteLine("\nZero is a special case, and cannot be factorized.");
            }
            else
            {
                string suffix = (factors.Length > 1)
                    ? $" are {String.Join(", ", factors)}"
                    : "is only 1";

                Console.WriteLine($"\nThe factor(s) of {number} {suffix}.");
            }
        }

        public static void DisplayPrimes(int number)
        {
            Console.WriteLine($"\n{number} is a prime number.");
        }

        public static void DisplayPerfect(int number)
        {
            Console.WriteLine($"\n{number} is a perfect number.");
        }

        public static void DisplayNotPrimeOrPerfect(int number)
        {
            Console.WriteLine($"\n{number} is not prime or perfect.");
        }

        public static void DisplayEndMessage()
        {
            Console.WriteLine($"\nPress any key to exit.");
            Console.ReadKey();
        }

    }
}