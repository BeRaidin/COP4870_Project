using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Models
{
    public class Module
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ContentItem> Content { get; set; }

        public Module() 
        { 
            Name = string.Empty;
            Description = string.Empty;
            Content = new List<ContentItem>();
        }

        public virtual string Display => $"{Name} - {Description}";
    }
}
