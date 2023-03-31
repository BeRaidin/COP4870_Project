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
        public int TotalItems;
        public Module() 
        { 
            Name = string.Empty;
            Description = string.Empty;
            Content = new List<ContentItem>();
            TotalItems = 0;
        }

        public virtual string Display => $"{Name} - {Description}";

        public void Add(ContentItem item)
        {
            Content.Add(item);
            item.Id = TotalItems.ToString();
            TotalItems++;
        }

        public void Remove(ContentItem item)
        {
            Content.Remove(item);
        }
    }
}
