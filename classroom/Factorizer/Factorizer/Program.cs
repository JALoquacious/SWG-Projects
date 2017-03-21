using System;
using System.Collections.Generic;

namespace Factorizer
{
    class Program
    {
        static void Main(string[] args)
        {
            int number = GetNumberFromUser();
            List<int> numberList = Calculator.PrintFactors(number);

            Calculator.IsPerfectNumber(number, numberList);

            Calculator.IsPrimeNumber(number, numberList);

            Console.WriteLine("Press any key to quit...");
            Console.ReadKey();
        }

        // Prompt the user for an integer & ensure validity
        static int GetNumberFromUser()
        {
            Console.WriteLine("Welcome to the Factorizer.");
            do
            {
                Console.Write("Please enter an integer: ");

                int number;
                string input = Console.ReadLine();
                bool valid = int.TryParse(input, out number);

                if (valid)
                {
                    return number;
                }
                else
                {
                    Console.WriteLine("That was not an integer.");
                }
            } while (true);
        }
    }

    class Calculator
    {
        // Given a number, print its factors per the specification
        public static List<int> PrintFactors(int number)
        {
            int i;
            string suffix;
            List<int> factors = new List<int>();

            for (i = 2; i < number; i++)
            {
                if (number % i == 0)
                {
                    factors.Add(i);
                }
            }

            suffix = (factors.Count > 1)
                ? $" are {String.Join(", ", factors)}"
                : "don't exist";

            Console.WriteLine($"The factors of {i} {suffix}.");
            return factors;
        }

        // Given a number, print if it is perfect or not
        public static void IsPerfectNumber(int number, List<int> factors)
        {
            int sum = 1;
            foreach (int factor in factors)
            {
                sum += factor;
            }
            if (sum == number)
            {
                Console.WriteLine($"{number} is a perfect number.");
            }
        }

        // Given a number, print if it is prime or not
        public static void IsPrimeNumber(int number, List<int> factors)
        {
            if (factors.Count == 0)
            {
                Console.WriteLine($"{number} is a prime number.");
            }
        }
    }
}
