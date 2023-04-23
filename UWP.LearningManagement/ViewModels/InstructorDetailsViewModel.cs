using UWP.Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UWP.LearningManagement.Dialogs;
using UWP.Library.LearningManagement.Database;
using System.Linq;

namespace UWP.LearningManagement.ViewModels
{
    public class InstructorDetailsViewModel
    {
        private readonly PersonService personService;

        private List<Course> CourseList
        { 
            get { return SelectedPerson.Courses; } 
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

        public InstructorDetailsViewModel()
        {
            personService = PersonService.Current;
            SelectedPerson = new Person();
            Courses = new ObservableCollection<Course>(CourseList);
            SubmittedAssignments = new ObservableCollection<GradesDictionary>();
            CurrentInstructor = SelectedPerson;
            GetAssignments();
            if (SelectedPerson as Instructor != null)
            {
                Type = "Instructor";
            }
            else if (SelectedPerson as TeachingAssistant != null) 
            {
                Type = "Teaching Assistant";
            }
        }

        public InstructorDetailsViewModel(int id)
        {
            personService = PersonService.Current;
            SelectedPerson = FakeDataBase.People.FirstOrDefault(i => i.Id == id);
            CurrentInstructor = SelectedPerson;
            Courses = new ObservableCollection<Course>(CourseList);
            SubmittedAssignments = new ObservableCollection<GradesDictionary>();
            GetAssignments();
            if (SelectedPerson as Instructor != null)
            {
                Type = "Instructor";
            }
            else if (SelectedPerson as TeachingAssistant != null)
            {
                Type = "Teaching Assistant";
            }
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
          //  SelectedCourse = null;
            var dialog = new InstructorJoinCourseDialog();
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
           // SelectedCourse = SelectedGrade.Course;
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
            foreach(var course in CourseList) 
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
            foreach (var course in CourseList) 
            { 
                Courses.Add(course);
            }
            GetAssignments();
        }
    }
}
