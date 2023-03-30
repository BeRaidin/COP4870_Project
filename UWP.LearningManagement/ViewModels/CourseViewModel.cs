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
using Windows.ApplicationModel.VoiceCommands;
using Windows.Foundation.Collections;

namespace UWP.LearningManagement.ViewModels
{
    public class CourseViewModel
    {
        private CourseService courseService;
        private PersonService personService;
        public string Name
        {
            get
            {
                return courseService.CurrentCourse.Name;
            }
            set
            {
                courseService.CurrentCourse.Name = value;
            }
        }
        public string Code
        {
            get
            {
                return Course.Code;
            }
            set
            {
                Course.Code = value;
            }
        }
        public string Room
        {
            get { return Course.Room; }
            set { Course.Room = value; }
        }
        private List<Person> allInstructors;
        private ObservableCollection<Person> _instructors;
        public ObservableCollection<Person> Instructors { get; set; }
        public List<Person> Roster 
        {
            get
            {
                return Course.Roster;
            }
            set
            {
                Course.Roster = value; 
            }
        }
        public Course Course
        {
            get
            {
                return courseService.CurrentCourse;
            }
            set
            {
                courseService.CurrentCourse = value;
            }
        }
        public string Hours { get; set; }

        public CourseViewModel()
        {
            courseService = CourseService.Current;
            personService = PersonService.Current;
            allInstructors = new List<Person>();
            foreach (var person in personService.PersonList)
            {
                if (person as Student == null)
                {
                    allInstructors.Add(person);
                }
            }
            Instructors = new ObservableCollection<Person>(allInstructors);
            foreach(var person in Instructors)
            {
                person.IsSelected = false; 
            }
        }

        public void Set()
        {
            foreach (Person person in Instructors)
            {
                if(person.IsSelected)
                {
                    Roster.Add(person);
                    person.AddCourse(Course);
                    if(person as Student != null)
                    {
                        (person as Student).FinalGrades.Add(Course, 0);
                    }
                }
            }

            if(int.TryParse(Hours, out int hours))
            {
                Course.CreditHours = hours;
            }
        }

        public void Add()
        {
            Set();
            courseService.Add(Course);
        }

        public void Edit()
        {
            courseService.CurrentCourse.Name = Name;
            courseService.CurrentCourse.Code = Code;
            foreach (var instructor in Instructors) 
            { 
                if(instructor.IsSelected && !courseService.CurrentCourse.Roster.Contains(instructor)) 
                {
                    courseService.CurrentCourse.Roster.Add(instructor);
                    instructor.Courses.Add(Course);
                }
                else if (!instructor.IsSelected && courseService.CurrentCourse.Roster.Contains(instructor))
                {
                    courseService.CurrentCourse.Roster.Remove(instructor);
                    instructor.Courses.Remove(Course);
                }
            }
        }

        public void FillChecks()
        {
            foreach(var instructor in Instructors)
            {
                if(courseService.CurrentCourse.Roster.Contains(instructor))
                {
                    instructor.IsSelected = true;
                }
            }
        }
    }
}
