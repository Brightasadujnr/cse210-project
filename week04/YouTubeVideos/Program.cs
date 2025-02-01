using System;
using System.Collections.Generic;

public class Comment
{
    public string CommenterName { get; set; }
    public string CommentText { get; set; }

    public Comment(string commenterName, string commentText)
    {
        CommenterName = commenterName;
        CommentText = commentText;
    }

    public override string ToString()
    {
        return $"{CommenterName}: {CommentText}";
    }
}

public class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Length { get; set; } 
    public List<Comment> Comments { get; set; }

    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        Length = length;
        Comments = new List<Comment>();
    }

    public void AddComment(string commenterName, string commentText)
    {
        Comment comment = new Comment(commenterName, commentText);
        Comments.Add(comment);
    }

    public int GetNumberOfComments()
    {
        return Comments.Count;
    }

    public void DisplayVideoDetails()
    {
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Length: {Length} seconds");
        Console.WriteLine($"Number of Comments: {GetNumberOfComments()}");
        Console.WriteLine("Comments:");
        foreach (var comment in Comments)
        {
            Console.WriteLine($"  - {comment}");
        }
        Console.WriteLine("----------------------------------------------");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Video video1 = new Video("Fishing in the Atlantic", "Captain Joe", 716);
        Video video2 = new Video("Learing how to cook", "Jane Smith", 1200);
        Video video3 = new Video("Machine Learning Basics", "Alice Johnson", 900);

        video1.AddComment("Kwaku", "Great tutorial!");
        video1.AddComment("Charlie", "Awesome fishing tips.");
        video1.AddComment("Ek", "I learned a lot from this.");

        video2.AddComment("James", "Awesome content!");
        video2.AddComment("Bright", "This is exactly what I needed.");
        video2.AddComment("Jakopo", "Clear and concise explanation.");

        video3.AddComment("Vindy", "Fantastic introduction to ML!");
        video3.AddComment("Alex", "Loved the examples.");
        video3.AddComment("Angel", "Can't wait for the next video.");

        List<Video> videos = new List<Video> { video1, video2, video3 };

        foreach (var video in videos)
        {
            video.DisplayVideoDetails();
        }
    }
}