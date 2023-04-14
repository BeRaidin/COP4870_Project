﻿using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UWP.LearningManagement.ViewModels
{
    internal class RosterViewModel
    {
        private readonly CourseService courseService;
        private readonly PersonService personService;
        private readonly List<Person> allStudents;

        public ObservableCollection<Person> Students { get; set; }
        public Course Course 
        {
            get { return courseService.CurrentCourse; }
        }

        public RosterViewModel()
        {
            courseService = CourseService.Current;
            personService = PersonService.Current;
            allStudents = new List<Person>();
            foreach(var person in personService.People)
            {
                if(person as Student != null)
                {
                    allStudents.Add(person);
                }
            }
            Students = new ObservableCollection<Person>(allStudents);
            foreach (var student in Students)
            {
                if(Course.Roster.Contains(student))
                {
                    student.IsSelected = true;
                }
                else
                {
                    student.IsSelected = false;
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
                }
                else if (student.IsSelected == true && !Course.Roster.Contains(student)) 
                { 
                    Course.Roster.Add(student);
                }
            }
        }
    }
}
