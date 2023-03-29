using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Models
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

        public virtual string Display => $"[{Id}] - {Title}";
    }
}
