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
    internal class RosterViewModel
    {
        private readonly CourseService courseService;
        private readonly PersonService personService;
        private List<Person> allStudents;
        private ObservableCollection<Person> _students;
        public ObservableCollection<Person> Students
        {
            get { return _students; }
            set { _students = value; }
        }
        public Course Course { get; set; }

        public RosterViewModel()
        {
            courseService = CourseService.Current;
            personService = PersonService.Current;
            Course = courseService.CurrentCourse;
            allStudents = new List<Person>();
            foreach(var student in personService.PersonList)
            {
                if(student as Student != null)
                {
                    allStudents.Add(student);
                }
            }
            Students = new ObservableCollection<Person>(allStudents);
            foreach (var person in Course.Roster)
            {
                if(Course.Roster.Contains(person))
                {
                    person.IsSelected = true;
                }
                else
                {
                    person.IsSelected = false;
                }
            }
        }

        public void AddRoster()
        {
            foreach(var student in Students)
            {
                if (student.IsSelected == false && Course.Roster.Contains(student))
                {
                    Course.Roster.Remove(student);
                    (student as Student).Remove(Course);
                }
                else if (student.IsSelected == true && !Course.Roster.Contains(student)) 
                { 
                    Course.Roster.Add(student);
                    (student as Student).Add(Course);
                }
            }
        }
    }
}
