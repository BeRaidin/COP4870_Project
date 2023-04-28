using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP.Library.LearningManagement.Models
{
    public class Semester
    {
        public List<Person> People;
        public List<Course> Courses;
        public List<Assignment> Assignments;
        public List<Announcement> Announcements;
        public List<Module> Modules;
        public List<ContentItem> ContentItems;

        public string Period { get; set; }
        public int Year { get; set; }
        public int Id { get; set; }

        public Semester()
        {
            People = new List<Person>();
            Courses = new List<Course>();
            Assignments = new List<Assignment>();
            Announcements = new List<Announcement>();
            Modules = new List<Module>();
            ContentItems = new List<ContentItem>();
        }

        public virtual string Display => $"{Period} {Year}";

        public void SetSemester(List<Course> courses, List<Person> people)
        {
            foreach (Course course in courses)
            {
                var newCourse = new Course
                {
                    Code = course.Code,
                    Name = course.Name,
                    Room = course.Room,
                    CreditHours = course.CreditHours,
                    Description = course.Description
                };
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
                newPerson.FirstName = person.FirstName;
                newPerson.LastName = person.LastName;
                People.Add(newPerson);
            }
        }

        public void Remove(Person removedPerson)
        {
            foreach (var person in People)
            {
                if (person.Id == removedPerson.Id)
                {
                    foreach (var course in person.Courses)
                    {
                        course.Roster.Remove(person);
                    }
                    People.Remove(person);
                    break;
                }
            }
        }

        public void Remove(Course removedCourse)
        {
            foreach (var course in Courses)
            {
                if (course.Code == removedCourse.Code)
                {
                    foreach (var person in course.Roster)
                    {
                        person.Courses.Remove(course);
                    }
                    Courses.Remove(course);
                    break;
                }
            }

        }
    }
}
