﻿using Library.LearningManagement.Model;
using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UWP.LearningManagement.Dialogs;

namespace UWP.LearningManagement.ViewModels
{
    public class StudentDetailsViewModel
    {
        private readonly PersonService personService;
        private readonly CourseService courseService;
        private readonly SemesterService semesterService;

        private List<Semester> SemesterList
        { 
            get { return semesterService.SemesterList; } 
        }
        public ObservableCollection<Semester> Semesters { get; set; }
        public Person SelectedPerson
        {
            get { return personService.CurrentPerson; }
            set { personService.CurrentPerson = value; }
        }
        public Assignment SelectedAssignment
        {
            get { return personService.CurrentAssignment; }
            set { personService.CurrentAssignment = value; }
        }
        public Course SelectedCourse
        {
            get { return courseService.CurrentCourse; }
            set { courseService.CurrentCourse = value; }
        }
        public GradesDictionary SelectedGrade { get; set; }

        public Student Student { get; set; }
        public string FirstName
        {
            get { return Student.FirstName; }
        }
        public string LastName
        {
            get { return Student.LastName; }
        }
        public List<Course> Courses 
        {
            get { return Student.Courses; }
        }
        public string Id
        {
            get { return Student.Id; }
        }
        public string GradeLevel { get; set; }
        public ObservableCollection<GradesDictionary> UnsubmittedGrades { get; set; }
        public ObservableCollection<GradesDictionary> GradedGrades { get; set; }
        public double GradePoint
        {
            get { return Student.GradePointAverage; }
        }
        public Dictionary<Course, double> FinalGrades
        {
            get { return Student.FinalGrades; }
        }

        public StudentDetailsViewModel()
        {
            personService = PersonService.Current;
            courseService = CourseService.Current;
            semesterService = SemesterService.Current;
            Student = SelectedPerson as Student;
            Semesters = new ObservableCollection<Semester>(SemesterList);
            UnsubmittedGrades = new ObservableCollection<GradesDictionary>();
            GradedGrades = new ObservableCollection<GradesDictionary>();
            Refresh();
            SetGradeLevel();
        }

        public void SetGradeLevel()
        {
            
            if (Student.Classification == Student.Classes.Freshman)
            {
                GradeLevel = "Freshman";
            }
            else if (Student.Classification == Student.Classes.Sophmore)
            {
                GradeLevel = "Sophmore";
            }
            else if (Student.Classification == Student.Classes.Junior)
            {
                GradeLevel = "Junior";
            }
            else if (Student.Classification == Student.Classes.Senior)
            {
                GradeLevel = "Senior";
            }
            else GradeLevel = "ERROR";
        }

        public async void DropClasses()
        {
            foreach(var course in Student.Courses)
            {
                course.IsSelected = false;
            }
            var dialog = new DropCoursesDialog();
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            Refresh();
        }

        public void SubmitAssignment()
        {
            if(SelectedGrade != null) 
            { 
                SelectedGrade.isSubmitted = true;
            }
            Refresh();
        }

        public bool CanDrop()
        {
            if (SelectedPerson.Courses.Count == 0)
            {
                return false;
            }
            else return true;
        }

        public void Refresh()
        {
            UnsubmittedGrades.Clear();
            GradedGrades.Clear();
            foreach (var grade in Student.Grades)
            {
                if (grade.isGraded == false && grade.isSubmitted == false)
                {
                    UnsubmittedGrades.Add(grade);
                }
                else if (grade.isGraded == true)
                {
                    GradedGrades.Add(grade);
                }
            }
        }
    }
}