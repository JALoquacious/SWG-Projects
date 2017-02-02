using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculatorKata
{
    public class Calculator
    {
        public int Add(string numbers)
        {
            int sum = 0;

            if (String.IsNullOrEmpty(numbers))
            {
                return sum;
            }
            else if (numbers.Contains(','))
            {
                string[] elements = numbers.Split(',');
                sum += int.Parse(elements[0]);
                sum += int.Parse(elements[1]);
            }
            return sum;
        }
    }
}
