using Library.LearningManagement.Services;
using UWP.Library.LearningManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UWP.LearningManagement.API.Util;
using Newtonsoft.Json;
using UWP.Library.LearningManagement.DTO;

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
                var returnVal = JsonConvert.DeserializeObject<List<StudentDTO>>(payload).Select(d => new StudentViewModel(d));
                return returnVal;
            }
        }
        
        public ObservableCollection<StudentViewModel> Students { get; set; }
        public Person SelectedPerson
        {
            get { return personService.CurrentPerson; }
            set { personService.CurrentPerson = value; }
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

                IEnumerable<StudentViewModel> searchResults = AllStudents.Where(i => i.Dto.FirstName.Contains(Query, StringComparison.InvariantCultureIgnoreCase)
                                                    || i.Dto.Id.Contains(Query, StringComparison.InvariantCultureIgnoreCase));

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
