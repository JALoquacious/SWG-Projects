using System;
using System.Threading;

namespace FlooringBusiness.BLL.Infrastructure
{
    public static class Extensions
    {
        public static string ToTitle(this string input)
        {
            if (input == null) return null;

            return (input.Length > 1)
                ? Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(input.ToLower())
                : input.ToUpper();
        }

        public static string Color(this string input, string colorString)
        {
            ConsoleColor colorValue;

            if (Enum.TryParse(colorString, true, out colorValue))
            {
                Console.ForegroundColor = colorValue;
            }
            return input;
        }
    }
}
