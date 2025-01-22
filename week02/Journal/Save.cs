class Save : Menu
{
    private List<string> entries;

    public Save(List<string> entries)
    {
        this.entries = entries;
    }
    public override void Execute()
    {
        Console.Write("Enter file name to save: ");
        string fileName = Console.ReadLine();

        try
        {
            using (StreamWriter outputFile = new StreamWriter(fileName))
            {
                DateTime theCurrentTime = DateTime.Now;
                string dateText = theCurrentTime.ToShortDateString();

                outputFile.WriteLine($"Journal saved on {dateText}");

                foreach (string entry in entries)
                {
                    outputFile.WriteLine(entry);
                }
            }
            Console.WriteLine($"\nEntries saved to {fileName} successfully!\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError saving to file: {ex.Message}\n");
        }
    }
}
