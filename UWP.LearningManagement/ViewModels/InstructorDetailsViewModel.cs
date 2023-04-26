using UWP.Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UWP.LearningManagement.Dialogs;
using UWP.Library.LearningManagement.Database;
using System.Linq;
using Newtonsoft.Json;
using UWP.LearningManagement.API.Util;
using System.Collections;
using System.Threading.Tasks;

namespace UWP.LearningManagement.ViewModels
{
    public class InstructorDetailsViewModel
    {
        private readonly int Id;
        private List<Instructor> InstructorList
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Person/GetInstructors").Result;
                return JsonConvert.DeserializeObject<List<Instructor>>(payload).ToList();
            }
        }
        private List<TeachingAssistant> AssistantList

        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Person/GetAssistants").Result;
                return JsonConvert.DeserializeObject<List<TeachingAssistant>>(payload).ToList();
            }
        }
        
        public ObservableCollection<CourseViewModel> Courses { get; set; }
        public Person Instructor { get; set; }

        public CourseViewModel SelectedCourse { get; set; }
        public GradesDictionary SelectedGrade { get; set; }

        public string Type { get; set; }
        public ObservableCollection<GradesDictionary> SubmittedAssignments { get; set; }


        public CourseViewModel CurrentCourse { get; set; }

        public InstructorDetailsViewModel() { }

        public InstructorDetailsViewModel(int id)
        {
            Id = id;
            Instructor = InstructorList.FirstOrDefault(i => i.Id == Id);
            if (Instructor == null)
            {
                Instructor = AssistantList.FirstOrDefault(i => i.Id == Id);
                Type = "Teaching Assistant";
            }
            else
            {
                Type = "Instructor";
            }
            Courses = new ObservableCollection<CourseViewModel>();
            foreach(var course in Instructor.Courses)
            {
                Courses.Add(new CourseViewModel(Instructor.Id, course.Id));
            }

            SubmittedAssignments = new ObservableCollection<GradesDictionary>();
            GetAssignments();
        }

        //public async void AddCourse()
        //{
        //    SelectedCourse = new Course();
        //    var dialog = new CourseDialog();
        //    if (dialog != null)
        //    {
        //        await dialog.ShowAsync();
        //    }
        //    if (!dialog.TestValid())
        //    {
        //        var errorDialog = new ErrorDialog();
        //        if (errorDialog != null)
        //        {
        //            await errorDialog.ShowAsync();
        //        }
        //    }
        //    Refresh();
        //}

        public async void JoinCourse()
        {

            var dialog = new InstructorJoinCourseDialog(Id);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            Refresh();
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
        public bool CanDrop()
        {
            if (Instructor.Courses.Count == 0)
            {
                return false;
            }
            else return true;
        }

        public async void GradeAssignment()
        {
            //personService.CurrentAssignment = SelectedGrade.Assignment;
            //Instructor = SelectedGrade.Person;
            //SelectedCourse = SelectedGrade.Course;
            var dialog = new GradeDialog();
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            //SelectedPerson = Instructor;
            SelectedGrade.IsGraded = true;
            Refresh();
        }

        public void GetAssignments()
        {
            SubmittedAssignments.Clear();
            foreach (var course in Instructor.Courses)
            {
                foreach (var person in course.Roster)
                {
                    if (person is Student student)
                    {
                        foreach (var assignment in student.Grades)
                        {
                            if (assignment.CourseCode == course.Code && assignment.IsGraded == false
                                && assignment.IsSubmitted == true)
                            {
                                SubmittedAssignments.Add(assignment);
                            }
                        }
                    }
                }
            }
        }

        public void Refresh()
        {
            if(Type == "Teaching Assistant")
            {
                Instructor = AssistantList.FirstOrDefault(x => x.Id == Id);
            }
            else Instructor = InstructorList.FirstOrDefault(x => x.Id == Id);


            Courses.Clear();
            foreach (var course in Instructor.Courses)
            {
                Courses.Add(new CourseViewModel(Instructor.Id, course.Id));
            }
            GetAssignments();
        }
    }
}
