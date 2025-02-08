using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Activities Menu:");
            Console.WriteLine("1. Start breathing Activity");
            Console.WriteLine("2. Start reflection Activity");
            Console.WriteLine("3. Start listing Activity");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an activity:");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    BreathingActivity breathingActivity = new BreathingActivity();
                    breathingActivity.Start();
                    break;
                case "2":
                    ReflectionActivity reflectionActivity = new ReflectionActivity();
                    reflectionActivity.Start();
                    break;
                case "3":
                    ListingActivity listingActivity = new ListingActivity();
                    listingActivity.Start();
                    break;
                case "4":
                    Console.WriteLine("Thank you for using the Mindfulness Program. Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    Thread.Sleep(1000);
                    break;
            }
        }
    }
}

abstract class Activity
{
    protected string Name { get; set; }
    protected string Description { get; set; }
    protected int Duration { get; set; }

    protected void DisplayStartingMessage()
    {
        Console.Clear();
        Console.WriteLine($"Welcome to the {Name} Activity\n");
        Console.WriteLine(Description);
        Console.Write("\nPlease enter the duration of the activity in seconds: ");
        Duration = int.Parse(Console.ReadLine());
        Console.WriteLine("Prepare to begin...");
        DisplayAnimation(3);
    }

    protected void DisplayEndingMessage()
    {
        Console.WriteLine("Well done! You have completed the activity.");
        DisplayAnimation(3);
        Console.WriteLine($"You have completed the {Name} Activity for {Duration} seconds.");
        DisplayAnimation(3);
    }

    protected void DisplayAnimation(int seconds, string type = "spinner")
    {
        DateTime endTime = DateTime.Now.AddSeconds(seconds);
        if (type == "spinner")
        {
            string[] spinner = { "|", "/", "-", "\\" };
            while (DateTime.Now < endTime)
            {
                foreach (string s in spinner)
                {
                    Console.Write($"\r{s} ");
                    Thread.Sleep(250);
                }
            }
        }
        else if (type == "countdown")
        {
            for (int i = seconds; i > 0; i--)
            {
                Console.Write(i);
                Thread.Sleep(1000);
                Console.Write("\b \b");
            }
        }
        Console.WriteLine();
    }
}

class BreathingActivity : Activity
{
    public BreathingActivity()
    {
        Name = "Breathing";
        Description = "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.";
    }

    public void Start()
    {
        DisplayStartingMessage();
        DateTime endTime = DateTime.Now.AddSeconds(Duration);
        while (DateTime.Now < endTime)
        {
            Console.WriteLine("Breathe in...");
            DisplayAnimation(4, "countdown");
            Console.WriteLine("Breathe out...");
            DisplayAnimation(4, "countdown");
        }
        DisplayEndingMessage();
    }
}

class ReflectionActivity : Activity
{
    private string[] prompts = {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private string[] questions = {
        "What is the day you would never forget?",
        "What do you love doing most?",
        "When was the last time you felt the Spirit?",
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectionActivity()
    {
        Name = "Reflection";
        Description = "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.";
    }

    public void Start()
    {
        DisplayStartingMessage();

        
        Random random = new Random();
        Console.WriteLine(prompts[random.Next(prompts.Length)]);
        Console.WriteLine("Press Enter when you are ready to continue...");
        Console.ReadLine(); 

        int questionCount = Math.Min(2, questions.Length);
        for (int i = 0; i < questionCount; i++)
        {
            Console.WriteLine(questions[random.Next(questions.Length)]);
            Console.WriteLine("Think about the question, then press Enter to continue...");
            Console.ReadLine();
        }

        DisplayEndingMessage();
    }
}

class ListingActivity : Activity
{
    private string[] prompts = {
        "Who are you good to this week?",
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity()
    {
        Name = "Listing";
        Description = "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.";
    }

    public void Start()
    {
        DisplayStartingMessage();
        Random random = new Random();
        Console.WriteLine(prompts[random.Next(prompts.Length)]);
        Console.WriteLine("You have a few seconds to prepare...");
        DisplayAnimation(5, "countdown");

        Console.WriteLine("Start listing items:");
        DateTime endTime = DateTime.Now.AddSeconds(Duration);
        int itemCount = 0;
        while (DateTime.Now < endTime)
        {
            Console.Write("> ");
            Console.ReadLine();
            itemCount++;
        }

        Console.WriteLine($"You listed {itemCount} items.");
        DisplayEndingMessage();
    }
}