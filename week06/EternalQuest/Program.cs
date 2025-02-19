using System;
using System.Collections.Generic;
using System.IO;

namespace EternalQuest
{
    // Base class for all goals
    abstract class Goal
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Points { get; set; }
        public bool IsCompleted { get; set; }

        public Goal(string name, string description, int points)
        {
            Name = name;
            Description = description;
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
        public SimpleGoal(string name, string description, int points)
            : base(name, description, points) { }

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
            return IsCompleted
                ? $"[X] {Name}: {Description}"
                : $"[ ] {Name}: {Description}";
        }

        public override string GetGoalType() => "SimpleGoal";
    }

    // Eternal goal class
    class EternalGoal : Goal
    {
        public EternalGoal(string name, string description, int points)
            : base(name, description, points) { }

        public override void RecordEvent()
        {
            Console.WriteLine($"You recorded progress for the eternal goal: {Name} and earned {Points} points.");
        }

        public override string GetStatus()
        {
            return $"[ ] {Name}: {Description}";
        }

        public override string GetGoalType() => "EternalGoal";
    }

    // Checklist goal class
    class ChecklistGoal : Goal
    {
        public int TargetCount { get; set; }
        public int CurrentCount { get; set; }
        public int BonusPoints { get; set; }

        public ChecklistGoal(string name, string description, int points, int targetCount, int bonusPoints)
            : base(name, description, points)
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
            return IsCompleted
                ? $"[X] {Name}: {Description} (Completed {CurrentCount}/{TargetCount} times)"
                : $"[ ] {Name}: {Description} (Completed {CurrentCount}/{TargetCount} times)";
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

                if (Goals[goalIndex] is ChecklistGoal checklistGoal && checklistGoal.IsCompleted)
                {
                    TotalScore += checklistGoal.BonusPoints;
                }
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
            try
            {
                using (StreamWriter writer = new StreamWriter(filename))
                {
                    writer.WriteLine(TotalScore);
                    foreach (var goal in Goals)
                    {
                        writer.WriteLine($"{goal.GetGoalType()}|{goal.Name}|{goal.Description}|{goal.Points}|{goal.IsCompleted}");
                        if (goal is ChecklistGoal checklistGoal)
                        {
                            writer.WriteLine($"{checklistGoal.TargetCount}|{checklistGoal.BonusPoints}|{checklistGoal.CurrentCount}");
                        }
                    }
                }
                Console.WriteLine("Progress saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving progress: {ex.Message}");
            }
        }

        public void LoadProgress(string filename)
        {
            try
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
                            string description = parts[2];
                            int points = int.Parse(parts[3]);
                            bool isCompleted = bool.Parse(parts[4]);

                            switch (goalType)
                            {
                                case "SimpleGoal":
                                    Goals.Add(new SimpleGoal(name, description, points) { IsCompleted = isCompleted });
                                    break;
                                case "EternalGoal":
                                    Goals.Add(new EternalGoal(name, description, points));
                                    break;
                                case "ChecklistGoal":
                                    string checklistData = reader.ReadLine();
                                    string[] checklistParts = checklistData.Split('|');
                                    int targetCount = int.Parse(checklistParts[0]);
                                    int bonusPoints = int.Parse(checklistParts[1]);
                                    int currentCount = int.Parse(checklistParts[2]);

                                    var checklistGoal = new ChecklistGoal(
                                        name,
                                        description,
                                        points,
                                        targetCount,
                                        bonusPoints)
                                    {
                                        CurrentCount = currentCount,
                                        IsCompleted = isCompleted
                                    };
                                    Goals.Add(checklistGoal);
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading progress: {ex.Message}");
            }
        }

        public int GetTotalScore()
        {
            return TotalScore;
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
                Console.WriteLine($"Total Points: {quest.GetTotalScore()}\n");
                Console.WriteLine("Menu Options:");
                Console.WriteLine("1. Create New Goal");
                Console.WriteLine("2. Record Event");
                Console.WriteLine("3. List Goals");
                Console.WriteLine("4. Save Progress");
                Console.WriteLine("5. Load Progress");
                Console.WriteLine("6. Quit");
                Console.Write("Please select a choice from the menu list: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Choose goal type:");
                        Console.WriteLine("1. Simple Goal");
                        Console.WriteLine("2. Eternal Goal");
                        Console.WriteLine("3. Checklist Goal");
                        Console.Write("Enter choice: ");
                        string goalType = Console.ReadLine();

                        Console.Write("Enter goal name: ");
                        string name = Console.ReadLine();

                        Console.Write("Enter goal description: ");
                        string description = Console.ReadLine();

                        Console.Write("Enter points: ");
                        if (!int.TryParse(Console.ReadLine(), out int points))
                        {
                            Console.WriteLine("Invalid points. Please enter a valid number.");
                            break;
                        }

                        switch (goalType)
                        {
                            case "1":
                                quest.AddGoal(new SimpleGoal(name, description, points));
                                break;
                            case "2":
                                quest.AddGoal(new EternalGoal(name, description, points));
                                break;
                            case "3":
                                Console.Write("Enter target count: ");
                                if (!int.TryParse(Console.ReadLine(), out int targetCount))
                                {
                                    Console.WriteLine("Invalid target count. Please enter a valid number.");
                                    break;
                                }
                                Console.Write("Enter bonus points: ");
                                if (!int.TryParse(Console.ReadLine(), out int bonusPoints))
                                {
                                    Console.WriteLine("Invalid bonus points. Please enter a valid number.");
                                    break;
                                }
                                quest.AddGoal(new ChecklistGoal(name, description, points, targetCount, bonusPoints));
                                break;
                            default:
                                Console.WriteLine("Invalid goal type.");
                                break;
                        }
                        break;

                    case "2":
                        quest.DisplayGoals();
                        Console.Write("\nEnter the goal number to record: ");
                        if (int.TryParse(Console.ReadLine(), out int goalIndex))
                        {
                            quest.RecordEvent(goalIndex - 1);
                        }
                        else
                        {
                            Console.WriteLine("Invalid goal number.");
                        }
                        break;

                    case "3":
                        quest.DisplayGoals();
                        break;

                    case "4":
                        Console.Write("\nEnter filename to save progress: ");
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