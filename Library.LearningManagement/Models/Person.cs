using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
 
        public Person() {
            Name = string.Empty;
        }

        public override string ToString()
        {
            return $"[{Id}] {Name}";
        }
    }
}