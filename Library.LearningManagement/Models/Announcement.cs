﻿namespace Library.LearningManagement.Models
{
    public class Announcement
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }

        public Announcement() 
        {
            Id = 0;
            Title = string.Empty;
            Message = string.Empty;
        }

        public void ChangeTitle()
        {
            Console.WriteLine("What is the title of the announcement?");
            Title = Console.ReadLine() ?? string.Empty;
        }

        public void ChangeMessage()
        {
            Console.WriteLine("What is the message?");
            Message = Console.ReadLine() ?? string.Empty;
        }

        public override string ToString()
        {
            return $"[{Id}] - {Title}";
        }
    }
}
