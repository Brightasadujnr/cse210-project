using System;

class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int>();
        int userInput;

        Console.WriteLine("Enter numbers (enter 0 to stop):");

        // Collect numbers from the user
        do
        {
            Console.Write("Enter a number: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out userInput) && userInput != 0)
            {
                numbers.Add(userInput);
            }
        } while (userInput != 0);

        // Compute the sum
        int sum = 0;
        foreach (int number in numbers)
        {
            sum += number;
        }

        // Compute the average
        double average = 0;
        if (numbers.Count > 0)
        {
            average = (double)sum / numbers.Count;
        }

        // Find the maximum number
        int max = numbers.Count > 0 ? numbers.Max() : 0;

        // Output the results
        Console.WriteLine($"The sum of the numbers is: {sum}");
        Console.WriteLine($"The average of the numbers is: {average:F2}");
        Console.WriteLine($"The maximum number is: {max}");
    }
}