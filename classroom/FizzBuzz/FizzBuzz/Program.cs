using System;

namespace FizzBuzz
{
    class Program
    {
        static void Main(string[] args)
        {
            int i;
            for (i = 1; i < 100; i++)
            {
                bool fizz = (i % 3 == 0);
                bool buzz = (i % 5 == 0);
                bool fizzbuzz = (i % 3 == 0 && i % 5 == 0);

                if (fizzbuzz)  Console.WriteLine(i + ": FizzBuzz");
                else if (fizz) Console.WriteLine(i + ": Fizz");
                else if (buzz) Console.WriteLine(i + ": Buzz");
                else Console.WriteLine(i);
            }
            Console.ReadLine();
        }
    }
}
