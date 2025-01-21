public class Resume
{
    public string Name { get; set; }
    public List<Job> _jobs = new List<Job>(); // List of Job objects

    // Constructor
    public Resume(string name)
    {
        Name = name;
    }

    // Method to display the resume
    public void DisplayResume()
    {
        Console.WriteLine($"Name: {Name}\n");
        Console.WriteLine("Jobs:");

        foreach (var job in _jobs)
        {
            job.Display();
        }
    }
    
}