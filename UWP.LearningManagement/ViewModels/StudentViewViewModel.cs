using Library.LearningManagement.Services;
using UWP.Library.LearningManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UWP.LearningManagement.API.Util;
using Newtonsoft.Json;

namespace UWP.LearningManagement.ViewModels
{
    public class StudentViewViewModel
    {
        private readonly PersonService personService;
        private readonly SemesterService semesterService;

        public IEnumerable<StudentViewModel> AllStudents
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Student").Result;
                var returnVal = JsonConvert.DeserializeObject<List<Student>>(payload).Select(d => new StudentViewModel(d));
                return returnVal;
            }
        }
        
        public ObservableCollection<StudentViewModel> Students { get; set; }
        public StudentViewModel SelectedViewModel
        {
            get
            {
                return new StudentViewModel(personService.CurrentPerson as Student);
            }
            set
            {
                personService.CurrentPerson = value.Student;
            }
        }
        public Semester SelectedSemester
        {
            get { return semesterService.CurrentSemester; }
        }
        public string Query { get; set; }
        public string Period
        { 
            get { return SelectedSemester.Period; } 
        }
        public int Year
        {
            get { return SelectedSemester.Year; }
        }

        public StudentViewViewModel() 
        {
            personService = PersonService.Current;
            semesterService = SemesterService.Current;
            Students = new ObservableCollection<StudentViewModel>(AllStudents);
        }

        public void Search()
        {
            if (Query != null && Query != "")
            {

                IEnumerable<StudentViewModel> searchResults = AllStudents.Where(i => i.Student.FirstName.Contains(Query, StringComparison.InvariantCultureIgnoreCase)
                                                    || i.Student.Id.Contains(Query, StringComparison.InvariantCultureIgnoreCase));

                Students.Clear();
                foreach (var person in searchResults)
                {
                    Students.Add(person);
                }
            }
            else
            {
                Refresh();
            }
        }

        public void Refresh()
        {
            Students.Clear();
            foreach (var person in AllStudents)
            {
                Students.Add(person);
            }
        }
    }
}
