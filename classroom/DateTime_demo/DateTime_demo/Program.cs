using System;

namespace DateTime_demo
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime date = GetDate();
            bool isMonday = IsMonday(date);
            DateTime friday = FindNextFriday(date).Date;
            TimeSpan interval = friday - date;

            Console.WriteLine(date.ToLongDateString());
            Continue();

            Console.WriteLine($"The date entered is {(isMonday ? "" : "not")} a Monday.");
            Continue();

            Console.WriteLine($"The next friday is {friday}.");
            Continue();

            Console.WriteLine($"The time interval in hours between {date} and");
            Console.WriteLine($"its following Friday is {interval.TotalHours}.");
            Continue();
        }

        static void Continue()
        {
            Console.WriteLine("Press <Enter> to continue...");
            Console.ReadLine();
        }

        static DateTime GetDate()
        {
            DateTime date;
            bool invalid = true;

            do
            {
                Console.Write("Enter a given date (and time, if you wish): ");
                string input = Console.ReadLine();

                if (DateTime.TryParse(input, out date))
                {
                    invalid = false;
                }
                else
                {
                    Console.WriteLine("That was not a valid input.");
                }
            } while (invalid);

            return date;
        }

        static bool IsMonday(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Monday;
        }

        static DateTime FindNextFriday(DateTime date)
        {
            do
            {
                date = date.AddDays(1);
            } while (date.DayOfWeek != DayOfWeek.Friday);

            return date;
        }
    }
}
