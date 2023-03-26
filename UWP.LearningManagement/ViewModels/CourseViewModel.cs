using Library.LearningManagement.Models;
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
        private Course course { get; set; }
        private List<Course> courses;
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
        public CourseViewModel(List<Course> courses)
        {
            course = new Course();
            this.courses = courses;
        }

        public void Add()
        {
            courses.Add(course);
        }

        public void RemovePerson()
        {
            courses.Remove(course);
        }
    }
}
