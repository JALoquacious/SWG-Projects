using System;
using System.Collections.Generic;

namespace GuessingGame
{
    class Program
    {
        private static int getLevel()
        {
            Dictionary<char, string> charToLevel = new Dictionary<char, string>()
            {
                {'e', "easy"},
                {'n', "normal"},
                {'h', "hard"}
            };

            do
            {
                Console.WriteLine("Please enter a difficulty level.");
                string difficultyLevel = Console.ReadLine().ToLower();

                if (difficultyLevel == "q") Environment.Exit(0);
                else if (difficultyLevel == "e") return 5;
                else if (difficultyLevel == "n") return 20;
                else if (difficultyLevel == "h") return 50;
                else
                {
                    Console.WriteLine("That was not a valid selection.");
                    Console.WriteLine("You must make a selection as follows:");
                    foreach (KeyValuePair<char, string> kvp in charToLevel)
                    {
                        Console.WriteLine($"{kvp.Key}: {kvp.Value}");
                    }
                }
            } while (true);
        }
        static void Main(string[] args)
        {
            int theAnswer;
            int playerGuess;
            int topOfRange;
            int guessCount = 0;
            string playerInput;
            string playerName;
            Random generateNumber = new Random();

            Console.WriteLine("Please enter your name.");
            playerName = Console.ReadLine();
            topOfRange = getLevel();
            theAnswer = generateNumber.Next(1, topOfRange + 1); // answer based on difficulty chosen

            do
            {
                // get player input
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Okay, {playerName}, enter your guess (1-{topOfRange}): ");
                playerInput = Console.ReadLine().ToLower();

                // attempt to convert the string to a number
                if (playerInput == "q")
                {
                    Environment.Exit(0);
                }
                else if (int.TryParse(playerInput, out playerGuess))
                {
                    guessCount++;

                    if (playerGuess == theAnswer)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{theAnswer} was the number. You win, {playerName}!");
                        if(guessCount == 1)
                        {
                            Console.WriteLine("Brilliant! You got it on the first try.");
                        }
                        break;
                    }
                    else
                    {
                        if (playerGuess < 1 || playerGuess > topOfRange)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"You're not even in the ballpark, {playerName}.");
                            Console.WriteLine($"Input must be between 1 and {topOfRange}.");
                        }
                        else if (playerGuess > theAnswer)
                        {
                            Console.WriteLine($"Your guess was too high, {playerName}!");
                        }
                        else if (playerGuess < theAnswer)
                        {
                            Console.WriteLine($"Your guess was too low, {playerName}!");
                        }
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("That wasn't a number!");
                }

                Console.WriteLine($"The # of guesses so far is {guessCount}");

            } while (true);

            Console.WriteLine("Press any key to quit.");
            Console.ReadKey();
        }
    }
}
