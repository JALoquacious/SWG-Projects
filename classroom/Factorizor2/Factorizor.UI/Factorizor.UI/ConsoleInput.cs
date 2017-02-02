using System;
using Factorizor.BLL;

namespace Factorizor.UI
{
    public class ConsoleInput
    {
        public static int GetNumberFromUser()
        {
            do
            {
                Console.Write("\nPlease give me a positive integer: ");

                int number;
                string input = Console.ReadLine();
                bool valid = int.TryParse(input, out number);

                if (valid)
                {
                    if (number < 0)
                    {
                        Console.WriteLine("Number cannot be less than zero.");
                    }
                    else
                    {
                        return number;
                    }
                }
                else
                {
                    Console.WriteLine("That was not an integer.");
                }
                
            } while (true);
        }
    }
}
