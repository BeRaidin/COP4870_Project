using UWP.Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UWP.Library.LearningManagement.Database;

namespace UWP.LearningManagement.ViewModels
{
    public class UnenrollStudentsViewModel
    {
        private readonly SemesterService semesterService;
        private readonly List<Person> allStudents;

        public Semester SelectedSemester
        {
            get { return semesterService.CurrentSemester; }
        }
        public ObservableCollection<Person> Students { get; set; }
        public string Query { get; set; }

        public UnenrollStudentsViewModel()
        {
            semesterService = SemesterService.Current;
            allStudents = new List<Person>();
            foreach(var person in SelectedSemester.People)
            {
                if(person as Student != null)
                {
                    person.IsSelected = false;
                    allStudents.Add(person);
                }
            }
            Students = new ObservableCollection<Person>(allStudents);
        }

        public void Search()
        {
            if (Query != null && Query != "")
            {

                IEnumerable<Person> searchResults = Students.Where(i => i.FirstName.Contains(Query, StringComparison.InvariantCultureIgnoreCase)
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

        public void Delete()
        {
            foreach (var person in allStudents)
            {
                if(person.IsSelected == true)
                {
                    foreach(var testPerson in FakeDataBase.People)
                    {
                        if (testPerson.FirstName == person.FirstName && testPerson.LastName == person.LastName && testPerson.Id == person.Id)
                        {
                            FakeDataBase.People.Remove(testPerson);
                            break;
                        }
                    }
                    semesterService.Remove(person);
                }
            }
            Refresh();
        }

        public void Refresh()
        {
            Students.Clear();
            foreach (var student in allStudents)
            {
                Students.Add(student);
            }
        }
    }
}
