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
        public List<Course> Courses { get; set; }


        public Person() {
            Name = string.Empty;
            Courses = new List<Course>();
        }

        public virtual string Display => $"[{Id}] {Name}";

        public void UpdateId(int num, List<Person> people)
        {
            while (people.Any(i => i.Id == num))
            {
                num++;
            }
            Id = num;
        }

        public void UpdateName()
        {
            Console.WriteLine("What is the name of the person?");
            Name = Console.ReadLine() ?? string.Empty;
        }

        public void AddCourse(Course course)
        {
            Courses.Add(course);
        }
    }
}