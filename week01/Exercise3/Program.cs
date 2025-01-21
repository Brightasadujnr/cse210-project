using System;

class Program
{
    static void Main(string[] args)
    {
       
       
        Random random = new Random();

        
        int magicNumber = random.Next(1, 101);

        int userGuess = 0; 
        Console.WriteLine("Welcome to the Guess My Number game!");
        Console.WriteLine("I have picked a magic number between 1 and 100.");
        Console.WriteLine("Try to guess it!");

        while (userGuess != magicNumber)
        {
            Console.Write("Enter your guess: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out userGuess))
            {
                if (userGuess == magicNumber)
                {
                    Console.WriteLine("Congratulations! You've being able to guess the magic number!");
                }
                else if (userGuess > magicNumber)
                {
                    Console.WriteLine("Is high, Try a lower number.");
                }
                else
                {
                    Console.WriteLine("Is low, Try higher number.");
                }
            }
            else
            {
                Console.WriteLine("Please enter a number.");
            }
        }
    }
}