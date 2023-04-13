using Library.LearningManagement.Model;
using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWP.LearningManagement.Dialogs;

namespace UWP.LearningManagement.ViewModels
{
    public class InstructorDetailsViewModel
    {
        private readonly PersonService personService;
        private readonly CourseService courseService;
        private readonly SemesterService semesterService;

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

        public Course SelectedCourse
        {
            get { return courseService.CurrentCourse; }
            set { courseService.CurrentCourse = value; }
        }
        public GradesDictionary SelectedGrade { get;set; }

        public string FirstName
        {
            get { return SelectedPerson.FirstName; }
        }
        public string LastName
        {
            get { return SelectedPerson.LastName; }
        }
        public string Id
        {
            get { return SelectedPerson.Id; }
        }
        public string Type { get; set; }
        public ObservableCollection<GradesDictionary> SubmittedAssignments { get; set; }

        public InstructorDetailsViewModel()
        {
            personService = PersonService.Current;
            courseService = CourseService.Current;
            semesterService = SemesterService.Current;
            Courses = new ObservableCollection<Course>(CourseList);
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

        public async void AddCourse()
        {
            SelectedCourse = new Course();
            var dialog = new CourseDialog();
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            Refresh();
        }

        public async void JoinCourse()
        {
            SelectedCourse = null;
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
            SelectedCourse = SelectedGrade.Course;
            var dialog = new GradeDialog();
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            SelectedPerson = CurrentInstructor;
            SelectedGrade.Assignment.isGraded = true;
            Refresh();
        }

        public void GetAssignments()
        {
            SubmittedAssignments = new ObservableCollection<GradesDictionary>();
            foreach(var course in CourseList) 
            { 
                foreach(var person in course.Roster)
                {
                    var student = person as Student;
                    if (student != null)
                    {
                        foreach(var assignment in student.Grades)
                        {
                            if(assignment.Course == course && assignment.Assignment.isGraded == false 
                                && assignment.Assignment.isSubmitted == true)
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
