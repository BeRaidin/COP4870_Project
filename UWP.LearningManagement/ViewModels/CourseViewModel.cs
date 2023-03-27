using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UWP.LearningManagement.ViewModels
{
    public class CourseViewModel
    {
        private CourseService courseService;
        private PersonService personService;
        private Course course { get; set; }
        public string Name
        {
            get
            {
                return course.Name;
            }
            set
            {
                course.Name = value;
            }
        }
        public string Code
        {
            get
            {
                return course.Code;
            }
            set
            {
                course.Code = value;
            }
        }
        public List<Person> Roster 
        {
            get
            {
                return course.Roster;
            }
            set
            { 
                course.Roster = value; 
            }
        }
        public Course Course
        {
            get
            {
                return course;
            }
            set
            {
                course = value;
            }
        }
        public List<Person>  People { get; set; }
        

        public CourseViewModel()
        {
            courseService = CourseService.Current;
            personService = PersonService.Current;
            People = personService.People;
            course = new Course();
        }

        public void SetRoster()
        {
            foreach (Person person in People)
            {
                if(person.IsSelected)
                {
                    Roster.Add(person);
                }
            }
        }

        public void Add()
        {
            SetRoster();
            courseService.Add(course);
        }

        public void Edit()
        {
            courseService.CurrentCourse.Name = course.Name;
            courseService.CurrentCourse.Code = course.Code;
        }
    }
}
