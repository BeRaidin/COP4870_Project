using System.Collections.Generic;
using UWP.Library.LearningManagement.Models;

namespace UWP.Library.LearningManagement.Database
{
    public static class FakeDataBase
    {
        public static List<Person> people = new List<Person>
        {
            new Student {Id = "0", FirstName="Brayden", LastName="Lewis", Classification=Student.Classes.Sophmore},
            new Person {Id = "1", FirstName="Chris", LastName="Millls"}
        };

        private static readonly List<Course> courses = new List<Course>();
        private static readonly List<Semester> semesters = new List<Semester>();

        public static List<Student> Students 
        { 
            get 
            {
                var returnList = new List<Student>();
                foreach (var person in people) 
                {
                    if (person is Student student)
                    {
                        returnList.Add(student);
                    }
                }
                return returnList;
            }
        }
        public static List<Course> Courses
        {
            get { return courses; }
        }
        public static List<Semester> Semesters
        { 
            get { return semesters; } 
        }

        public static List<Person> People
        {
            get { return people; }

        }

        public static List<Person> Instructors
        {
            get
            {
                var returnList = new List<Person>();
                foreach (var person in people)
                {
                    if (person as Student == null)
                    {
                        returnList.Add(person);
                    }
                }
                return returnList;
            }
        }
    }
}
