class Write : Menu
{
    private List<string> entries;
    private List<string> prompts = new List<string>
    {
        "What was the highlight of your day?",
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "What are you grateful for today?",
        "Describe a challenge you faced and how you overcame it.",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };

    private Random random = new Random();

    public Write(List<string> entries)
    {
        this.entries = entries;
    }

    public override void Execute()
    {
        DateTime theCurrentTime = DateTime.Now;
        string dateText = theCurrentTime.ToShortDateString();

        string prompt = prompts[random.Next(prompts.Count)];
        Console.WriteLine($"\nPrompt: {prompt}");
        Console.Write("Your response: ");
        string response = Console.ReadLine();

        string entry = $"[{dateText} {theCurrentTime.ToShortTimeString()}]\nPrompt: {prompt}\nResponse: {response}\n";
        entries.Add(entry);
        Console.WriteLine("\nEntry added successfully!\n");
    }
}