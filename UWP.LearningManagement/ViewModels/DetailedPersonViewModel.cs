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
    public class DetailedPersonViewModel : INotifyPropertyChanged
    {
        private readonly PersonService personService;
        private readonly CourseService courseService;
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


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
            get { return SelectedPerson.Name; }
        }
        public List<Course> Courses 
        {
            get { return SelectedPerson.Courses; }
        }
        public string Type { get; set; }
        public string GradeLevel { get; set; }
        public ObservableCollection<GradesDictionary> _grades;
        public ObservableCollection<GradesDictionary> Grades
        {
            get { return _grades; }
            set {
                _grades = value;
            }
        }

        public double GradePoint
        {
            get { return Student.GradePointAverage; }
        }
        public Dictionary<Course, double> FinalGrades
        {
            get { return Student.FinalGrades; }
        }

        public DetailedPersonViewModel()
        {
            personService = PersonService.Current;
            courseService = CourseService.Current;
            Student = SelectedPerson as Student;
            if (Student != null)
            {
                Grades = new ObservableCollection<GradesDictionary>(Student.Grades);
                if (SelectedCourse != null)
                {
                    foreach (var grade in Student.Grades)
                    {
                        if (!SelectedCourse.Assignments.Contains(grade.Assignment))
                        {
                            Grades.Remove(grade);
                        }
                    }
                }
            }
            else Grades = new ObservableCollection<GradesDictionary>();
            

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
    }
}
