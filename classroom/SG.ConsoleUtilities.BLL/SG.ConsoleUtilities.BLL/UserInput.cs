using System;

namespace SG.ConsoleUtilities.BLL
{
    public class UserInput
    {
        public static int GetIntFromUser (string prompt)
        {
            int result;
            string userInput;

            do
            {
                Console.Write(prompt);
                userInput = Console.ReadLine();

                if (int.TryParse(userInput, out result))
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("That was not a valid input.");
                }

            } while (true);
        }
    }
}
