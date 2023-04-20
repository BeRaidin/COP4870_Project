using UWP.Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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
        public Student SelectedStudent
        {
            get { return personService.CurrentPerson as Student; }
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

        public string FirstName
        {
            get { return SelectedStudent.FirstName; }
        }
        public string LastName
        {
            get { return SelectedStudent.LastName; }
        }
        public List<Course> Courses 
        {
            get { return SelectedStudent.Courses; }
        }
        public string Id
        {
            get { return SelectedStudent.Id; }
        }
        public string GradeLevel { get; set; }
        public ObservableCollection<GradesDictionary> UnsubmittedGrades { get; set; }
        public ObservableCollection<GradesDictionary> GradedGrades { get; set; }
        public double GradePoint
        {
            get { return SelectedStudent.GradePointAverage; }
        }
        public Dictionary<Course, double> FinalGrades
        {
            get { return SelectedStudent.FinalGrades; }
        }

        public StudentDetailsViewModel(Student s)
        {
            personService = PersonService.Current;
            courseService = CourseService.Current;
            semesterService = SemesterService.Current;
            SelectedStudent = s;
            Semesters = new ObservableCollection<Semester>(SemesterList);
            UnsubmittedGrades = new ObservableCollection<GradesDictionary>();
            GradedGrades = new ObservableCollection<GradesDictionary>();
            Refresh();
            SetGradeLevel();
        }

        public void SetGradeLevel()
        {
            
            if (SelectedStudent.Classification == Student.Classes.Freshman)
            {
                GradeLevel = "Freshman";
            }
            else if (SelectedStudent.Classification == Student.Classes.Sophmore)
            {
                GradeLevel = "Sophmore";
            }
            else if (SelectedStudent.Classification == Student.Classes.Junior)
            {
                GradeLevel = "Junior";
            }
            else if (SelectedStudent.Classification == Student.Classes.Senior)
            {
                GradeLevel = "Senior";
            }
            else GradeLevel = "ERROR";
        }

        public async Task DropClasses()
        {
            foreach(var course in SelectedStudent.Courses)
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
                SelectedGrade.IsSubmitted = true;
            }
            Refresh();
        }

        public bool CanDrop()
        {
            if (SelectedStudent.Courses.Count == 0)
            {
                return false;
            }
            else return true;
        }

        public void Refresh()
        {
            UnsubmittedGrades.Clear();
            GradedGrades.Clear();
            foreach (var grade in SelectedStudent.Grades)
            {
                if (grade.IsGraded == false && grade.IsSubmitted == false)
                {
                    UnsubmittedGrades.Add(grade);
                }
                else if (grade.IsGraded == true)
                {
                    GradedGrades.Add(grade);
                }
            }
        }
    }
}
