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
        private readonly SemesterService semesterService;
        private readonly int Id;
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
        public Assignment SelectedAssignment
        {
            get { return personService.CurrentAssignment; }
            set { personService.CurrentAssignment = value; }
        }
        public GradesDictionary SelectedGrade { get; set; }

        public Student Student { get; set; }
        public string GradeLevel { get; set; }
        public ObservableCollection<GradesDictionary> UnsubmittedGrades { get; set; }
        public ObservableCollection<GradesDictionary> GradedGrades { get; set; }

        public StudentDetailsViewModel() { }

        public StudentDetailsViewModel(int id)
        {
            Id = id;
            personService = PersonService.Current;
            semesterService = SemesterService.Current;
            Student = StudentList.FirstOrDefault(i => i.Id == Id);
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
            if (CanDrop())
            {
                var dialog = new DropCoursesDialog(Id);
                if (dialog != null)
                {
                    await dialog.ShowAsync();
                }
                Refresh();
            }
        }

        public async void SubmitAssignment()
        {
            if (SelectedGrade != null)
            {
                SelectedGrade.Assignment.IsSubmitted = true;
                await new WebRequestHandler().Post("http://localhost:5159/Assignment/UpdateIsSelected", SelectedGrade.Assignment);
                await new WebRequestHandler().Post("http://localhost:5159/Person/UpdateStudentCourses", Student);
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
            Student = StudentList.FirstOrDefault(i => i.Id == Id);
            UnsubmittedGrades.Clear();
            GradedGrades.Clear();
            foreach (var grade in Student.Grades)
            {
                if (grade.Assignment.IsSubmitted == false && grade.Assignment.IsSubmitted == false)
                {
                    UnsubmittedGrades.Add(grade);
                }
                else if (grade.Assignment.IsGraded == true)
                {
                    GradedGrades.Add(grade);
                }
            }
        }
    }
}
