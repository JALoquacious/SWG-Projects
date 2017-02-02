using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculatorKata.BLL
{
    public class Calculator
    {
        int sum = 0;

        public string[] GetSeparators(string arguments)
        {
            List<string> delimiters = new List<string>() { ",", "\n" };

            if (arguments.StartsWith("//["))
            {
                int start = 3;
                int end = arguments.IndexOf("]");
                int length = end - start;
                string separator = arguments.Substring(start, length);
                delimiters.Add(separator);
            }
            return delimiters.ToArray();
        }
        public int Add(string arguments)
        {
            string[] delimiters = GetSeparators(arguments);
            bool separators = delimiters.Any(character => arguments.Contains(character));
            if (String.IsNullOrEmpty(arguments))
            {
                return sum;
            }
            if (delimiters.Length > 2)
            {
                string last = delimiters[delimiters.Length - 1];
                arguments = arguments.Remove(0, 4 + last.Length);
            }

            if (separators)
            {
                string[] elements = arguments.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < elements.Length; i++)
                {
                    sum += int.Parse(elements[i]);
                }
            }
            else
            {
                sum += int.Parse(arguments);
            }
            return sum;
        }
    }
}
