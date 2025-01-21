using System;

class Program
{
    static void Main(string[] args)
    {
        Resume resume = new Resume ("John Dow");
        
        Job job1 = new Job(
            title: "General Maniger",
            company: "Amazon",
            startYear: "2019",
            endYear: "2022"
        );

        Job job2 = new Job(
            title: "CEO",
            company: "Facebook",
            startYear: "2022",
            endYear: "2023"
        );

        

        resume._jobs.Add(job1);
        resume._jobs.Add(job2);

        resume.DisplayResume();
    }
}