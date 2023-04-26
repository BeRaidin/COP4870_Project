using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UWP.Library.LearningManagement.Models
{
    public class Module
    {
        public int Id { get; set; }
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

        public void Add(ContentItem item)
        {
            ContentItem previousItem = Content.FirstOrDefault(x => x.Id == item.Id);
            if(previousItem != null)
            {
                Content.Remove(previousItem);
            }
            Content.Add(item);
        }

        public void Remove(ContentItem item)
        {
            ContentItem removedItem = Content.FirstOrDefault(x => x.Id == item.Id);
            if (removedItem != null) { }
            {
                Content.Remove(removedItem);
            }
        }
    }
}
