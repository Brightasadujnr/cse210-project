public class Job
{
    public string Title { get; set; }
    public string Company { get; set; }
    public string StartYear { get; set; }
    public string EndYear { get; set; }

    // Constructor to initialize the job with full details
    public Job(string title, string company, string startYear, string endYear)
    {

        Title = title;
        Company = company;
        StartYear = startYear;
        EndYear = endYear;
    }
    public void Display()
    {
    
        Console.WriteLine($"{Title} ({Company}) {StartYear}-{EndYear}");
    }
    

}
