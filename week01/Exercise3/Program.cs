using System;

class Program
{
    static void Main(string[] args)
    {
        using System;

class Program
{
    static void Main()
    {
        // Create a random number generator
        Random random = new Random();

        // Generate a magic number between 1 and 100
        int magicNumber = random.Next(1, 101);

        int userGuess = 0; // Variable to store the user's guess
        Console.WriteLine("Welcome to the Guess My Number game!");
        Console.WriteLine("I have picked a magic number between 1 and 100.");
        Console.WriteLine("Try to guess it!");

        // Loop until the user guesses the magic number
        while (userGuess != magicNumber)
        {
            // Get the user's guess
            Console.Write("Enter your guess: ");
            string input = Console.ReadLine();

            // Validate and parse the input
            if (int.TryParse(input, out userGuess))
            {
                // Check if the guess is correct
                if (userGuess == magicNumber)
                {
                    Console.WriteLine("Congratulations! You guessed the magic number!");
                }
                else if (userGuess > magicNumber)
                {
                    Console.WriteLine("Too high! Try guessing lower.");
                }
                else
                {
                    Console.WriteLine("Too low! Try guessing higher.");
                }
            }
            else
            {
                // Handle invalid input
                Console.WriteLine("Please enter a valid number.");
            }
        }
    }
}

    }
}