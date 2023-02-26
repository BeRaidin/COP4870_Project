using System;
using System.Collections;
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

        public void ChangeId()
        {
            Console.WriteLine("What is the ID of the person");
            var id = Console.ReadLine();
            while (!int.TryParse(id, out int result))
            {
                Console.WriteLine("That is not a valid Id, please try again");
                id = Console.ReadLine();
            }
                Id = int.Parse(id);
        }

        public void ChangeName()
        {
            Console.WriteLine("What is the name of the person?");
            Name = Console.ReadLine() ?? string.Empty;
        }
    }
}