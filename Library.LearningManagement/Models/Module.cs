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

        public void ChangeName()
        {
            Console.WriteLine("What is the name of the module");
            Name = Console.ReadLine() ?? string.Empty;
        }

        public void ChangeDescription()
        {
            Console.WriteLine("What is the description of the module");
            Description = Console.ReadLine() ?? string.Empty;
        }

        public override string ToString()
        {
            return $"{Name} - {Description}" +
                 $"\n\tContent:\n{string.Join("\n", Content.Select(a => a.ToString()).ToArray())}";
        }
    }
}
