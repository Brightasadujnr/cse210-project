class Load : Menu
{
   private List<string> entries;

    public Load(List<string> entries)
    {
        this.entries = entries;
    }

    public override void Execute()
    {
        Console.Write("Enter file name to load: ");
        string fileName = Console.ReadLine();

        try
        {
            if (File.Exists(fileName))
            {
                entries.Clear();
                string[] lines = File.ReadAllLines(fileName);
                foreach (string line in lines)
                {
                    entries.Add(line); // Add each line to the entries list
                }
                Console.WriteLine($"\nEntries loaded from {fileName} successfully!\n");
            }
            else
            {
                Console.WriteLine($"\nFile {fileName} does not exist.\n");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError loading from file: {ex.Message}\n");
        }
    }
}