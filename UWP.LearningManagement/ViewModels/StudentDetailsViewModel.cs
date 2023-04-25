using UWP.Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using UWP.LearningManagement.Dialogs;
using UWP.Library.LearningManagement.Database;
using System.Linq;
using Newtonsoft.Json;
using UWP.LearningManagement.API.Util;

namespace UWP.LearningManagement.ViewModels
{
    public class StudentDetailsViewModel
    {
        private readonly PersonService personService;
        private readonly CourseService courseService;
        private readonly SemesterService semesterService;
        private List<Student> StudentList
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Person/GetStudents").Result;
                return JsonConvert.DeserializeObject<List<Student>>(payload).ToList();
            }
        }


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
        public int Id
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
        public List<FinalGradesDictionary> FinalGrades
        {
            get { return Student.FinalGrades; }
        }

        public StudentDetailsViewModel(){}

        public StudentDetailsViewModel(int id)
        {
            personService = PersonService.Current;
            courseService = CourseService.Current;
            semesterService = SemesterService.Current;
            SelectedPerson = StudentList.FirstOrDefault(i =>i.Id == id);
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

        public async Task DropClasses()
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
                SelectedGrade.IsSubmitted = true;
            }
            Refresh();
        }

        public bool CanDrop()
        {
            if (Student.Courses.Count == 0)
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
