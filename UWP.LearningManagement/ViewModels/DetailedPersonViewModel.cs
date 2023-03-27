using Library.LearningManagement.Services;
using Library.LearningManagement.Models;
using UWP.LearningManagement.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.UI.Xaml.Controls;
using Windows.Foundation.Collections;
using System.Reflection.Metadata;

namespace UWP.LearningManagement.ViewModels
{
    internal class DetailedPersonViewModel
    {
        private CourseService courseService;
        private PersonService personService;

        public string Name
        {
            get { return personService.CurrentPerson.Name; }
        }

        public List<Course> Courses 
        {
            get { return personService.CurrentPerson.Courses; }
        }

        public string Type { get; set; }
        public string GradeLevel { get; set; }

        public DetailedPersonViewModel()
        {
            courseService = CourseService.Current;
            personService = PersonService.Current;
            SetType();
            SetGradeLevel();
        }

        public void SetType()
        {
            if (personService.CurrentPerson as Student != null) 
            {
                Type = "Student";
            }
            else if (personService.CurrentPerson as Instructor != null)
            {
                Type = "Instructor";
            }
            else if (personService.CurrentPerson as TeachingAssistant != null)
            {
                Type = "Teaching Assistant";
            }
        }

        public void SetGradeLevel()
        {
            var student = personService.CurrentPerson as Student;
            if (student != null)
            {
                if (student.Classification == Student.Classes.Freshman)
                {
                    GradeLevel = "Freshman";
                }
                else if (student.Classification == Student.Classes.Sophmore)
                {
                    GradeLevel = "Sophmore";
                }
                else if (student.Classification == Student.Classes.Junior)
                {
                    GradeLevel = "Junior";
                }
                else if (student.Classification == Student.Classes.Senior)
                {
                    GradeLevel = "Senior";
                }
                else GradeLevel = "";
            }
        }




    }
}
