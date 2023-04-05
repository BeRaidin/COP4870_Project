using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Models
{
    public class Semester
    {
        public List<Course> Courses;
        public List<Person> People;

        public string Period { get; set; }
        public int Year { get; set; }

        public Semester( )
        {
            Courses = new List<Course>();
            People = new List<Person>();
        }

        public void SetSemester(List<Course> courses, List<Person> people)
        {
            foreach (Course course in courses)
            {
                var newCourse = new Course();
                newCourse.Code = course.Code;
                newCourse.Name = course.Name;
                newCourse.Room = course.Room;
                newCourse.CreditHours = course.CreditHours;
                newCourse.Description = course.Description;
                Courses.Add(newCourse);
            }
            foreach (Person person in people)
            {
                var newPerson = new Person();
                if (person as Student != null)
                {
                    newPerson = new Student();
                }
                else if (person as Instructor != null)
                {
                    newPerson = new Instructor();
                }
                else if (person as TeachingAssistant != null)
                {
                    newPerson = new TeachingAssistant();
                }

                newPerson.Id = person.Id;
                newPerson.Name = person.Name;
                People.Add(newPerson);
            }
        }
    }
}
