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
        public Course SelectedCourse { get; set; }
        public List<Person>  People { get; set; }
        

        public CourseViewModel()
        {
            courseService = CourseService.Current;
            personService = PersonService.Current;
            People = personService.People;
            course = new Course();
        }

        public CourseViewModel(Course selectedCourse)
        {
            courseService = CourseService.Current;
            personService = PersonService.Current;
            course = new Course();
            this.SelectedCourse = selectedCourse;
        }

        public void Add()
        {
            courseService.Add(course);
        }

        public void Remove()
        {
            courseService.Remove(SelectedCourse);
        }
        public void Edit()
        {
            SelectedCourse.Name = course.Name;
            SelectedCourse.Code = course.Code;
        }
    }
}
