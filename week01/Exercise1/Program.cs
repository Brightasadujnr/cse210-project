using System;

class Program
{
    static void Main(string[] args)
    {
<<<<<<< HEAD
        Job job1 = new Job("Software Engineer");
        Console.WriteLine("Hello World! This is the Exercise1 Project.");
=======
        Console.WriteLine("What is your first name? ");
        string first = Console.ReadLine();
        
        Console.WriteLine("Please What is your last name? ");
        string last = Console.ReadLine();

        Console.WriteLine($"Your name is {first}, {first} {last}.");
>>>>>>> b9b2a7af80973a405419bf6bdeeccb04e6451446
    }
}

using System;

class Program
{
    static void Main(string[] args)
    {
        // Create a new job instance with job title "Software Engineer"
        Job job1 = new Job("Software Engineer");

        // Output the job title
        Console.WriteLine(job1.JobTitle);
    }
}
