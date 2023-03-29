﻿using Library.LearningManagement.Services;
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
    internal class DetailedPersonViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private PersonService personService;
        public Student Student { get; set; }
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
        private Dictionary<Assignment, double> _grades;
        public Dictionary<Assignment, double> Grades
        {
            get { return _grades; }
            set {
                _grades = value;
                RaisePropertyChanged(nameof(Grades));
            }
        }
        private Assignment _assignmentKey;
        public Assignment AssignmentKey
        {
            get { return _assignmentKey; }
            set { 
                _assignmentKey = value;
                RaisePropertyChanged(nameof(AssignmentKey));}
        }
        public string Score { get; set; }



        public int Total
        {
            get { return AssignmentKey.TotalAvailablePoints; }
        }

        public DetailedPersonViewModel()
        {
            personService = PersonService.Current;
            Student = personService.CurrentPerson as Student;
            if (Student != null)
            {
                _grades = Student.Grades;
            }
            else _grades = new Dictionary<Assignment, double>();
            SetType();
            SetGradeLevel();
        }

        public DetailedPersonViewModel(Assignment assignment)
        {
            personService = PersonService.Current;
            Student = personService.CurrentPerson as Student;
            if (Student != null)
            {
                _grades = Student.Grades;
            }
            else _grades = new Dictionary<Assignment, double>();
            SetType();
            SetGradeLevel();
            AssignmentKey = assignment;
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

        public async void SetGrade()
        {
            var dialog = new GradeDialog(AssignmentKey);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
        }

        public void AddGrade()
        {
            if (double.TryParse(Score, out var value))
            {
                if (value > Total)
                {
                    Student.Grades[AssignmentKey] = value;
                }
                else if (value < 0)
                {
                    Student.Grades[AssignmentKey] = 0;
                }
                else Student.Grades[AssignmentKey] = value;
            }
        }
        
        public void ChangedSelectedAssignment(Assignment assignment)
        {
            AssignmentKey = assignment;
        }
    }
}
