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
    public class PersonViewModel
    {
        private CourseService courseService;
        private PersonService personService;
        private Person person { get; set; }
        public string Name { get; set; }
        public Person Person
        {
            get { return person; }
            set { person = value; }
        }
        public List<string> PersonTypes { get; set; }
        public string SelectedType { get; set; }
        public List<string> StudentLevels { get; set; }
        public string SlectedLevel { get; set; }

        public PersonViewModel()
        {
            courseService = CourseService.Current;
            personService = PersonService.Current;
            PersonTypes = new List<string>
            { "Student", "Instructor", "Teaching Assistant" };
            StudentLevels = new List<string>
            { "Freshman", "Sophmore", "Junior", "Senior" };
        }

        public void Set()
            {
            if (SelectedType == "Student")
            {
                Person = new Student();
            }
            else if (SelectedType == "Instructor")
            {
                Person = new Instructor();
            }
            else if (SelectedType == "Teaching Assistant")
            {
                Person = new TeachingAssistant();
            }
            else Person = new Person();
            Person.Name = Name;
            Person.Id = personService.Size();

        }


        public void Add()
        {
            Set();
            var Student = Person as Student;
            if (Student != null) 
            {
                if(SlectedLevel == "Freshman")
                {
                    Student.Classification = Student.Classes.Freshman;
                }
                else if(SlectedLevel == "Sophmore")
                {
                    Student.Classification = Student.Classes.Sophmore;
                }
                else if (SlectedLevel == "Junior")
                {
                    Student.Classification = Student.Classes.Junior;
                }
                else if (SlectedLevel == "Senior")
                {
                    Student.Classification = Student.Classes.Senior;
                }
                else Student.Classification = Student.Classes.Freshman;
                personService.Add(Student);
            }
            else personService.Add(Person);
        }


        public void Edit()
        {
            personService.CurrentPerson.Name = person.Name;
        }
    }
}
