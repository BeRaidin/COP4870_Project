using System.Collections.Generic;
using UWP.Library.LearningManagement.Models;

namespace UWP.Library.LearningManagement.Database
{
    public static class FakeDataBase
    {
        public static List<Person> people = new List<Person>
        {
            new Student {Id = 0, FirstName="Brayden", LastName="Lewis", Classification=Student.Classes.Sophmore},
            new Instructor {Id = 1, FirstName="Chris", LastName="Millls"},
            new TeachingAssistant {Id = 2, FirstName="Joe", LastName="Joey"}
        };

        private static readonly List<Course> courses = new List<Course>{
            new Course {Id = 0, Code="COP4530", Name="C#", CreditHours=4, Room="HCB"}
        };
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

        public static List<Instructor> Instructors
        {
            get
            {
                var returnList = new List<Instructor>();
                foreach (var person in people)
                {
                    if (person is Instructor instructor)
                    {
                        returnList.Add(instructor);
                    }
                }
                return returnList;
            }
        }

        public static List<TeachingAssistant> Assistants
        {
            get
            {
                var returnList = new List<TeachingAssistant>();
                foreach (var person in people)
                {
                    if (person is TeachingAssistant assistant)
                    {
                        returnList.Add(assistant);
                    }
                }
                return returnList;
            }
        }
    }
}
