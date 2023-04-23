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

namespace UWP.LearningManagement.ViewModels
{
    public class InstructorDetailsViewModel
    {
        private readonly PersonService personService;
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

        public ObservableCollection<Course> Courses { get; set; }

        public Person SelectedPerson
        {
            get { return personService.CurrentPerson; }
            set { personService.CurrentPerson = value; }
        }
        public Person CurrentInstructor { get; set; }

        public CourseViewModel SelectedCourse { get; set; }
        public GradesDictionary SelectedGrade { get;set; }

        public string Type { get; set; }
        public ObservableCollection<GradesDictionary> SubmittedAssignments { get; set; }


        public CourseViewModel CurrentCourse { get; set; }

        public InstructorDetailsViewModel() {}

        public InstructorDetailsViewModel(int id)
        {
            personService = PersonService.Current;
            SelectedPerson = InstructorList.FirstOrDefault(i => i.Id == id);
            if(SelectedPerson == null)
            {
                SelectedPerson = AssistantList.FirstOrDefault(i => i.Id == id);
                Type = "Teaching Assistant";
            }
            else
            {
                Type = "Instructor";
            }
            CurrentInstructor = SelectedPerson;
            Courses = new ObservableCollection<Course>(SelectedPerson.Courses);
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
            var dialog = new InstructorJoinCourseDialog(SelectedPerson.Id);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            Refresh();
        }

        public async void GradeAssignment()
        {
            personService.CurrentAssignment = SelectedGrade.Assignment;
            SelectedPerson = SelectedGrade.Person;
            //SelectedCourse = SelectedGrade.Course;
            var dialog = new GradeDialog();
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            SelectedPerson = CurrentInstructor;
            SelectedGrade.IsGraded = true;
            Refresh();
        }

        public void GetAssignments()
        {
            SubmittedAssignments.Clear();
            foreach(var course in SelectedPerson.Courses) 
            { 
                foreach(var person in course.Roster)
                {
                    if (person is Student student)
                    {
                        foreach (var assignment in student.Grades)
                        {
                            if (assignment.Course == course && assignment.IsGraded == false
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
            Courses.Clear();
            foreach (var course in SelectedPerson.Courses) 
            { 
                Courses.Add(course);
            }
            GetAssignments();
        }
    }
}
