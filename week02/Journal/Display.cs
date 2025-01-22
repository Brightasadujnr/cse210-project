class DisplayEntries : Menu
{
    private List<string> entries;

    public DisplayEntries(List<string> entries)
    {
        this.entries = entries;
    }

    public override void Execute()
    {
        if (entries.Count == 0)
        {
            Console.WriteLine("\nNo entries available to display.\n");
            return;
        }

        Console.WriteLine("\nJournal Entries:\n--");
        foreach (var entry in entries)
        {
            Console.WriteLine(entry);
        }
    }
}
