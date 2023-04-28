using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace UWP.Library.LearningManagement.Models
{
    public class Semester
    {
        public List<Student> Students { get; set; }
        public List<Instructor> Instructors { get; set; }
        public List<TeachingAssistant> Assistants { get; set; }
        public List<Course> Courses { get; set; }
        public List<Assignment> Assignments { get; set; }
        public List<Announcement> Announcements { get; set; }
        public List<Module> Modules { get; set; }
        public List<ContentItem> ContentItems { get; set; }

        public List<Person> People
        {
            get
            {
                List<Person> list = new List<Person>();
                foreach (var person in Students) 
                {
                    list.Add(person);
                }
                foreach (var person in Instructors)
                {
                    list.Add(person);
                }
                foreach (var person in Assistants)
                {
                    list.Add(person);
                }
                return list;
            }
        }

        public string Period { get; set; }
        public int Year { get; set; }
        public int Id { get; set; }

        public Semester()
        {
            Students = new List<Student>();
            Instructors = new List<Instructor>();
            Assistants = new List<TeachingAssistant>();
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
                if (person is Student student)
                {
                    Students.Add(student);
                }
                else if (person is Instructor instructor)
                {
                    Instructors.Add(instructor);
                }
                else if (person is TeachingAssistant assistant)
                {
                    Assistants.Add(assistant);
                }
            }
        }

        public void Remove(Person removedPerson)
        {
            if (removedPerson is Student removedStudent)
            {
                var student = Students.FirstOrDefault(x => x.Id == removedStudent.Id);
                foreach (var course in student.Courses)
                {
                    course.Roster.Remove(student);
                }
                Students.Remove(student);
            }
            else if (removedPerson is Instructor removedInstructor)
            {
                var instructor = Instructors.FirstOrDefault(x => x.Id == removedInstructor.Id);
                Instructors.Remove(instructor);
            }
            else if(removedPerson is TeachingAssistant removedAssistant)
            {
                var assistant = Assistants.FirstOrDefault(x => x.Id == removedAssistant.Id);
                Assistants.Remove(assistant);
            }
        }

        public void Remove(Course removedCourse)
        {
            var course = Courses.FirstOrDefault(x => x.Id ==removedCourse.Id);
            Courses.Remove(course);
        }
    }
}
