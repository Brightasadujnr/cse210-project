using System;

class Program
{

    static void Main(string[] args)
    {
        Console.Write("What is your grade percdntage? ");
        string answer = Console.ReadLine();
        int percent = int.Parse(answer);

        string letter = "";
        if (percent >= 90)
        {
            letter = "A";
        }
        else if (percent >=80)
        {
            letter = "B";
        }
        else if (percent >=70)
        {
            letter = "C";
        }
        else if (percent >=60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }
        
        Console.WriteLine($"You grade is: {letter}");

        if (percent >=70)
         {
            Console.WriteLine("Well done, You've passed!");
         }

        else
        {
            Console.Write("Sorry, try again next time.");
        }
    }
        
}