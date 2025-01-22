using System;

List<string> entries = new List<string>();
bool running = true;

string v = Console.ReadLine();

while (running)
{
    Console.WriteLine("Please select one of following options:");
    Console.WriteLine("1. Write an entry");
    Console.WriteLine("2. Display entries");
    Console.WriteLine("3. Save entries to file");
    Console.WriteLine("4. Load entries from file");
    Console.WriteLine("5. Quit");
    Console.Write("What would you like to do? ");

    string choice = v;

    Menu action = choice switch
    {
        "1" => new Write(entries),
        "2" => new DisplayEntries(entries),
        "3" => new Save(entries),
        "4" => new Load(entries),
        "5" => null,
        _ => null
    };

    if (action != null)
    {
        action.Execute();
    }
    else if (choice == "5")
    {
        running = false;
        Console.WriteLine("Goodbye!");
    }
    else
    {
        Console.WriteLine("Invalid choice. Please try again.");
    }
}
