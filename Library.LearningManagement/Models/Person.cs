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
        public int Id {  get; set; }

        public string Name { get; set; }
 
        public Person() {
            Name = string.Empty;
        }

        public override string ToString()
        {
            return $"[{Id}] {Name}";
        }

        public void ChangeId(int num, List<Person> people)
        {
            while (people.Any(i => i.Id == num))
            {
                num++;
            }
            Id = num;
        }

        public void ChangeName()
        {
            Console.WriteLine("What is the name of the person?");
            Name = Console.ReadLine() ?? string.Empty;
        }
    }
}