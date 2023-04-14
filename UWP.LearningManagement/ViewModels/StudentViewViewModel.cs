using Library.LearningManagement.Services;
using Library.LearningManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace UWP.LearningManagement.ViewModels
{
    public class StudentViewViewModel
    {
        private readonly PersonService personService;
        private readonly SemesterService semesterService;
        private readonly List<Person> allStudents;
        
        public ObservableCollection<Person> Students { get; set; }
        public Person SelectedPerson
        {
            get { return personService.CurrentPerson; }
            set { personService.CurrentPerson = value; }
        }
        public string Query { get; set; }
        public string Period
        { 
            get { return semesterService.CurrentSemester.Period; } 
        }
        public int Year
        {
            get { return semesterService.CurrentSemester.Year; }
        }

        public StudentViewViewModel() 
        {
            personService = PersonService.Current;
            semesterService = SemesterService.Current;
            allStudents = new List<Person>();
            foreach (var person in personService.People)
            {
                if (person as Student != null)
                {
                    allStudents.Add(person);
                }
            }
            Students = new ObservableCollection<Person>(allStudents);
        }

        public void Search()
        {
            if (Query != null && Query != "")
            {

                IEnumerable<Person> searchResults = allStudents.Where(i => i.FirstName.Contains(Query, StringComparison.InvariantCultureIgnoreCase)
                                                    || i.Id.Contains(Query, StringComparison.InvariantCultureIgnoreCase));

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
            foreach (var person in allStudents)
            {
                Students.Add(person);
            }
        }
    }
}
