using System;
using System.Configuration;
using System.Text.RegularExpressions;
using FlooringBusiness.Models.Enums;
using FlooringBusiness.Models.Prototypes;

namespace FlooringBusiness.BLL.Infrastructure
{
    public class Utilities
    {
        public const string ProgramTitle = "FLOORING ORDER MANAGEMENT SYSTEM";

        public static string GetString(string prompt, Regex specification, bool editMode)
        {
            while (true)
            {
                Console.Write($"\nEnter {prompt}: ");
                string input = Console.ReadLine();

                if (editMode && string.IsNullOrEmpty(input))
                {
                    return null;
                }

                if (ValidInput(input, specification))
                {
                    return input;
                }

                Console.WriteLine("That was not a valid input. Please try again.");
            }
        }

        public static DateTime GetDate(string prompt, bool isNewOrder)
        {
            while (true)
            {
                Console.Write($"\nEnter {prompt}. (<Enter> to use current date): ");

                string input = Console.ReadLine();
                DateTime output = DateTime.Now;

                if (String.IsNullOrEmpty(input))
                {
                    return output;
                }

                if (DateTime.TryParse(input, out output))
                {
                    if (isNewOrder && output.Date < DateTime.Now)
                    {
                        Console.WriteLine("Order dates must be in the future.");
                        continue;
                    }
                    return output;
                }

                Console.WriteLine("That was not a valid input. Please try again.");
            }
        }

        public static T GetNumber<T>(string prompt, Regex specification)
        {
            while (true)
            {
                Console.Write($"\nEnter {prompt}: ");
                string input = Console.ReadLine();

                if (ValidInput(input, specification))
                {
                    return (T) Convert.ChangeType(input, typeof(T));
                }

                Console.WriteLine("That was not a valid input. Please try again.");
            }
        }

        public static void GenerateSeparator(char repeater, int width, ConsoleColor color)
        {
            string line = String.Empty;

            Console.ForegroundColor = color;

            for (int i = 0; i < width; i++)
            {
                line += repeater;
            }

            Console.Write(line);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void PrintOrderDetails(Order order, WorkflowType type)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            ConsoleColor dark = (type == WorkflowType.Remove) ? ConsoleColor.DarkRed : ConsoleColor.Blue;
            ConsoleColor light = (type == WorkflowType.Remove) ? ConsoleColor.Red : ConsoleColor.Cyan;

            Console.Clear();
            GenerateSeparator('=', Console.WindowWidth, dark);
            Console.WriteLine("Order Details:");
            GenerateSeparator('=', Console.WindowWidth, dark);

            Console.WriteLine();

            GenerateSeparator('-', Console.WindowWidth, light);
            Indent(Bullet($" Order Number: {order.OrderNumber}"), 4);
            Indent(Bullet($" Customer:     {order.Customer}"), 4);
            Indent(Bullet($" Product:      {order.Product.ProductType}"), 4);
            Indent(Bullet($" Area:         {order.Area}"), 4);
            Indent(Bullet($" State:        {order.StateTax.StateAbbreviation}"), 4);
            Indent(Bullet($" Total Cost:   {order.Total:c}"), 4);
            GenerateSeparator('-', Console.WindowWidth, light);
        }

        public static bool ValidInput(string input, Regex specification)
        {
            return specification.IsMatch(input);
        }

        public static void Indent(string text, int start)
        {
            if (start == Console.WindowWidth / 2)
            {
                int spaces = start + (text.Length / 2);
                Console.WriteLine(text.PadLeft(spaces));
            }
            else
            {
                Console.WriteLine(text.PadLeft(text.Length + start));
            }
        }

        public static string Bullet(string text)
        {
            return text.PadLeft(text.Length + 1, '\u2022');
        }

        public static void Continue()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}