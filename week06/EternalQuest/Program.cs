using System;
using System.Collections.Generic;
using System.IO;

namespace EternalQuest
{
    // Base class for all goals
    abstract class Goal
    {
        public string Name { get; set; } // Made public
        public int Points { get; set; } // Made public
        public bool IsCompleted { get; set; } // Made public

        public Goal(string name, int points)
        {
            Name = name;
            Points = points;
            IsCompleted = false;
        }

        public abstract void RecordEvent();
        public abstract string GetStatus();
        public abstract string GetGoalType();
    }

    // Simple goal class
    class SimpleGoal : Goal
    {
        public SimpleGoal(string name, int points) : base(name, points) { }

        public override void RecordEvent()
        {
            if (!IsCompleted)
            {
                IsCompleted = true;
                Console.WriteLine($"Congratulations! You completed the goal: {Name} and earned {Points} points.");
            }
            else
            {
                Console.WriteLine("This goal has already been completed.");
            }
        }

        public override string GetStatus()
        {
            return IsCompleted ? $"[X] {Name}" : $"[ ] {Name}";
        }

        public override string GetGoalType() => "SimpleGoal";
    }

    // Eternal goal class
    class EternalGoal : Goal
    {
        public EternalGoal(string name, int points) : base(name, points) { }

        public override void RecordEvent()
        {
            Console.WriteLine($"You recorded progress for the eternal goal: {Name} and earned {Points} points.");
        }

        public override string GetStatus()
        {
            return $"[ ] {Name} (eternal)";
        }

        public override string GetGoalType() => "EternalGoal";
    }

    // Checklist goal class
    class ChecklistGoal : Goal
    {
        private int TargetCount { get; set; }
        private int CurrentCount { get; set; }
        private int BonusPoints { get; set; }

        public ChecklistGoal(string name, int points, int targetCount, int bonusPoints) : base(name, points)
        {
            TargetCount = targetCount;
            BonusPoints = bonusPoints;
            CurrentCount = 0;
        }

        public override void RecordEvent()
        {
            if (!IsCompleted)
            {
                CurrentCount++;
                Console.WriteLine($"You recorded progress for the checklist goal: {Name} and earned {Points} points.");

                if (CurrentCount >= TargetCount)
                {
                    IsCompleted = true;
                    Points += BonusPoints;
                    Console.WriteLine($"Congratulations! You completed the checklist goal: {Name} and earned a bonus of {BonusPoints} points.");
                }
            }
            else
            {
                Console.WriteLine("This checklist goal has already been completed.");
            }
        }

        public override string GetStatus()
        {
            return IsCompleted ? $"[X] {Name} (Completed {CurrentCount}/{TargetCount} times)" : $"[ ] {Name} (Completed {CurrentCount}/{TargetCount} times)";
        }

        public override string GetGoalType() => "ChecklistGoal";
    }

    // Program class to manage goals and scores
    class EternalQuestProgram
    {
        private List<Goal> Goals { get; set; }
        private int TotalScore { get; set; }

        public EternalQuestProgram()
        {
            Goals = new List<Goal>();
            TotalScore = 0;
        }

        public void AddGoal(Goal goal)
        {
            Goals.Add(goal);
        }

        public void RecordEvent(int goalIndex)
        {
            if (goalIndex >= 0 && goalIndex < Goals.Count)
            {
                Goals[goalIndex].RecordEvent();
                TotalScore += Goals[goalIndex].Points;
            }
            else
            {
                Console.WriteLine("Invalid goal index.");
            }
        }

        public void DisplayGoals()
        {
            Console.WriteLine("\nYour Goals:");
            for (int i = 0; i < Goals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Goals[i].GetStatus()}");
            }
            Console.WriteLine($"\nTotal Score: {TotalScore}\n");
        }

        public void SaveProgress(string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine(TotalScore);
                foreach (var goal in Goals)
                {
                    writer.WriteLine($"{goal.GetGoalType()}|{goal.Name}|{goal.Points}|{goal.IsCompleted}");
                    if (goal is ChecklistGoal checklistGoal)
                    {
                        writer.WriteLine($"{checklistGoal.GetStatus()}");
                    }
                }
            }
            Console.WriteLine("Progress saved successfully.");
        }

        public void LoadProgress(string filename)
        {
            if (File.Exists(filename))
            {
                using (StreamReader reader = new StreamReader(filename))
                {
                    TotalScore = int.Parse(reader.ReadLine());
                    Goals.Clear();

                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split('|');
                        string goalType = parts[0];
                        string name = parts[1];
                        int points = int.Parse(parts[2]);
                        bool isCompleted = bool.Parse(parts[3]);

                        switch (goalType)
                        {
                            case "SimpleGoal":
                                Goals.Add(new SimpleGoal(name, points) { IsCompleted = isCompleted });
                                break;
                            case "EternalGoal":
                                Goals.Add(new EternalGoal(name, points));
                                break;
                            case "ChecklistGoal":
                                string statusLine = reader.ReadLine();
                                Goals.Add(new ChecklistGoal(name, points, 0, 0) { IsCompleted = isCompleted });
                                break;
                        }
                    }
                }
                Console.WriteLine("Progress loaded successfully.");
            }
            else
            {
                Console.WriteLine("No saved progress found.");
            }
        }
    }

    // Main program
    class Program
    {
        static void Main(string[] args)
        {
            EternalQuestProgram quest = new EternalQuestProgram();
            bool running = true;

            while (running)
            {
                Console.WriteLine("Eternal Quest Program");
                Console.WriteLine("1. Create New Goal");
                Console.WriteLine("2. Record Event");
                Console.WriteLine("3. Display Goals");
                Console.WriteLine("4. Save Progress");
                Console.WriteLine("5. Load Progress");
                Console.WriteLine("6. Quit");
                Console.Write("Please select a choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Please choice what type of goal: ");
                        Console.WriteLine("1. Simple Goal");
                        Console.WriteLine("2. Eternal Goal");
                        Console.WriteLine("3. Checklist Goal");
                        Console.Write("Enter choice: ");
                        string goalType = Console.ReadLine();

                        Console.Write("Enter goal name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter points: ");
                        int points = int.Parse(Console.ReadLine());

                        switch (goalType)
                        {
                            case "1":
                                quest.AddGoal(new SimpleGoal(name, points));
                                break;
                            case "2":
                                quest.AddGoal(new EternalGoal(name, points));
                                break;
                            case "3":
                                Console.Write("Enter target count: ");
                                int targetCount = int.Parse(Console.ReadLine());
                                Console.Write("Enter bonus points: ");
                                int bonusPoints = int.Parse(Console.ReadLine());
                                quest.AddGoal(new ChecklistGoal(name, points, targetCount, bonusPoints));
                                break;
                            default:
                                Console.WriteLine("Invalid choice.");
                                break;
                        }
                        break;

                    case "2":
                        quest.DisplayGoals();
                        Console.Write("Enter the goal number to record: ");
                        int goalIndex = int.Parse(Console.ReadLine()) - 1;
                        quest.RecordEvent(goalIndex);
                        break;

                    case "3":
                        quest.DisplayGoals();
                        break;

                    case "4":
                        Console.Write("Enter filename to save progress: ");
                        string saveFile = Console.ReadLine();
                        quest.SaveProgress(saveFile);
                        break;

                    case "5":
                        Console.Write("Enter filename to load progress: ");
                        string loadFile = Console.ReadLine();
                        quest.LoadProgress(loadFile);
                        break;

                    case "6":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }
}