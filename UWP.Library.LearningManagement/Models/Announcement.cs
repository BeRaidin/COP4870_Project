namespace UWP.Library.LearningManagement.Models
{
    public class Announcement
    {
        public int Id { get; set; }
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
