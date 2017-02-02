using System.Collections.Generic;

namespace Factorizor.BLL
{
    public class Calculator
    {
        // Given a number, return its factors per the specification
        public int[] GetFactors(int number)
        {
            if (number == 0) return new int[] {0};
            else
            {
                int i;
                List<int> factors = new List<int>();

                for (i = 1; i < number; i++)
                {
                    if (number % i == 0)
                    {
                        factors.Add(i);
                    }
                }
                return factors.ToArray();
            }
        }

        // Given a number, return if it is perfect or not
        public bool IsPerfectNumber(int number, int[] factors)
        {
            int sum = 0;

            foreach (int factor in factors)
            {
                sum += factor;
            }
            return (number != 0 && sum == number);
        }

        // Given a number, return if it is prime or not
        public bool IsPrimeNumber(int number, int[] factors)
        {
            return (number != 0 &&  factors.Length == 1);
        }
    }
}
