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
    public class DetailedPersonViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly PersonService personService;
        private readonly CourseService courseService;

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
        public double GradePoint
        {
            get { return Student.GradePointAverage; }
        }

        public Dictionary<Course, double> FinalGrades
        {
            get { return Student.FinalGrades; }
            set { Student.FinalGrades = value; }
        }

        public int Total
        {
            get { return AssignmentKey.TotalAvailablePoints; }
        }

        public DetailedPersonViewModel()
        {
            personService = PersonService.Current;
            courseService = CourseService.Current;
            Student = SelectedPerson as Student;
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
            courseService = CourseService.Current;
            Student = SelectedPerson as Student;
            if (Student != null)
            {
                Grades = Student.Grades;
            }
            else Grades = new Dictionary<Assignment, double>();
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
            SelectedAssignment = AssignmentKey;
            var dialog = new GradeDialog();
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
        }
        
        public void ChangedSelectedAssignment(Assignment assignment)
        {
            AssignmentKey = assignment;
        }
    }
}
