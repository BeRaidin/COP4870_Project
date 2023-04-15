using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;

namespace UWP.LearningManagement.ViewModels
{
    internal class RosterViewModel
    {
        private readonly CourseService courseService;
        private readonly PersonService personService;
        private readonly SemesterService semesterService;
        private readonly List<Person> allStudents;

        public ObservableCollection<Person> Students { get; set; }
        public Semester SelectedSemester { get { return semesterService.CurrentSemester; } }
        public Course Course 
        {
            get { return courseService.CurrentCourse; }
        }

        public RosterViewModel()
        {
            courseService = CourseService.Current;
            personService = PersonService.Current;
            semesterService = SemesterService.Current;
            allStudents = new List<Person>();
            foreach(var person in SelectedSemester.People)
            {
                if(person as Student != null)
                {
                    allStudents.Add(person);
                }
            }
            Students = new ObservableCollection<Person>(allStudents);
            foreach (var student in Students)
            {
                if(Course.Roster.Contains(student))
                {
                    student.IsSelected = true;
                }
                else
                {
                    student.IsSelected = false;
                }
            }
        }

        public void AddRoster()
        {
            foreach(var student in Students)
            {
                if (student.IsSelected == false && Course.Roster.Contains(student))
                {
                    Course.Remove(student);
                }
                else if (student.IsSelected == true && !Course.Roster.Contains(student)) 
                { 
                    Course.Add(student);
                }
            }
        }
    }
}
