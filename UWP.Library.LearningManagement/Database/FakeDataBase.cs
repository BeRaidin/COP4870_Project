using System.Collections.Generic;
using UWP.Library.LearningManagement.Models;

namespace UWP.Library.LearningManagement.Database
{
    public static class FakeDataBase
    {
        private static List<Person> people = new List<Person>();
        private static List<Course> courses = new List<Course>();
        private static List<Semester> semesters = new List<Semester>();

        public static List<Person> People 
        { 
            get { return people; }
        }
        public static List<Course> Courses
        {
            get { return courses; }
        }
        public static List<Semester> Semesters
        { 
            get { return semesters; } 
        }
    }
}
