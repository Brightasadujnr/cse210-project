using System;

class Program
{
    static void Main(string[] args)
    {
        Job job1 = new Job("Software Engineer");
        Console.WriteLine("Hello World! This is the Exercise1 Project.");
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
