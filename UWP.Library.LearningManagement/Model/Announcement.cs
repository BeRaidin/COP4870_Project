namespace Library.LearningManagement.Models
{
    public class Announcement
    {
        public string Title { get; set; }
        public string Message { get; set; }

        public Announcement() 
        {
            Title = string.Empty;
            Message = string.Empty;
        }

        public virtual string Display => $"{Title}";
    }
}
