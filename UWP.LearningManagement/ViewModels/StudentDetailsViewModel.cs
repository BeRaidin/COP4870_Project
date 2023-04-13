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
using Library.LearningManagement.Model;

namespace UWP.LearningManagement.ViewModels
{
    public class StudentDetailsViewModel : INotifyPropertyChanged
    {
        private readonly PersonService personService;
        private readonly CourseService courseService;
        private readonly SemesterService semesterService;
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private List<Semester> SemesterList
        { get { return semesterService.SemesterList; } }
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
            get { return SelectedPerson.FirstName; }
        }
        public string LastName
        {
            get { return SelectedPerson.LastName; }
        }

        public List<Course> Courses 
        {
            get { return SelectedPerson.Courses; }
        }
        public string Id
        {
            get { return SelectedPerson.Id; }
        }
        public string Type { get; set; }
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
            SetType();
            SetGradeLevel();
        }

        public void SetType()
        {
            if (SelectedPerson as Student != null) 
            {
                Type = "Student";
            }
            else if (SelectedPerson as Instructor != null)
            {
                Type = "Instructor";
            }
            else if (SelectedPerson as TeachingAssistant != null)
            {
                Type = "Teaching Assistant";
            }
        }

        public void SetGradeLevel()
        {
            var student = SelectedPerson as Student;
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
            var grade = (SelectedPerson as Student).GetGradeDict(SelectedAssignment);
            var dialog = new GradeDialog();
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            RaisePropertyChanged("FinalGrades.Keys");
            RaisePropertyChanged("GradePoint");
            RaisePropertyChanged("grade.Grade");
        }

        public void ChangedSelectedAssignment(Assignment assignment)
        {
            SelectedAssignment = assignment;
        }

        public async void DropClasses()
        {
            foreach(var course in SelectedPerson.Courses)
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
                else if(grade.isGraded == true)
                {
                    GradedGrades.Add(grade);
                }
            }
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
    }
}
