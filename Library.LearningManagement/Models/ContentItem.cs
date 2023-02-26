using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Models
{
    public class ContentItem
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

        public ContentItem() 
        { 
            Name = string.Empty;
            Description = string.Empty;
        }

        public override string ToString()
        {
            return $"{Name} - {Description}";
        }
    }
}
