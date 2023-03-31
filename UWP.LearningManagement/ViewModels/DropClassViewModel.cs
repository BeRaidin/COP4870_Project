﻿using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace UWP.LearningManagement.ViewModels
{
    public class DropClassViewModel
    {
        private readonly PersonService personService;

        public Person Person
        {
            get { return personService.CurrentPerson; }
            set { personService.CurrentPerson = value; }
        }
        public ObservableCollection<Course> Courses { get; set; }

        public DropClassViewModel()
        {
            personService = PersonService.Current;
            Courses = new ObservableCollection<Course>(Person.Courses);
        }

        public void Drop()
        {
            foreach(var course in Person.Courses.ToList())
            {
                if (course.IsSelected)
                {
                    course.Roster.Remove(Person);
                    Person.Remove(course);
                }
            }
        }
    }
}