using System;
using System.Collections.Generic;
using Factorizor.BLL;

namespace Factorizor.UI
{
    public class WorkFlow
    {
        public void Factorize()
        {
            Calculator calc = new Calculator();

            ConsoleOutput.DisplayTitle();

            int number = ConsoleInput.GetNumberFromUser();

            // do calculations
            int[] factors = calc.GetFactors(number);
            bool isPrime = calc.IsPrimeNumber(number, factors);
            bool isPerfect = calc.IsPerfectNumber(number, factors);

            // output results
            ConsoleOutput.DisplayFactors(number, factors);
            if (isPrime) ConsoleOutput.DisplayPrimes(number);
            if (isPerfect) ConsoleOutput.DisplayPerfect(number);
            if (!isPrime && !isPerfect) ConsoleOutput.DisplayNotPrimeOrPerfect(number);
            
            ConsoleOutput.DisplayEndMessage();
        }
    }
}
