﻿using Library.LearningManagement.Models;
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
        public List<Person>  People { get; set; }
        public string Hours { get; set; }

        public CourseViewModel()
        {
            courseService = CourseService.Current;
            personService = PersonService.Current;
            People = personService.PersonList;
            foreach(var person in People)
            {
                person.IsSelected = false; 
            }
        }

        public void Set()
        {
            foreach (Person person in People)
            {
                if(person.IsSelected)
                {
                    Roster.Add(person);
                    person.AddCourse(Course);
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
        }
    }
}
