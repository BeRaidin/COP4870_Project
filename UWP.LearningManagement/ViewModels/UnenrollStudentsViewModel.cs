using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWP.Library.LearningManagement.Database;

namespace UWP.LearningManagement.ViewModels
{
    public class UnenrollStudentsViewModel
    {
        private readonly PersonService personService;
        private readonly SemesterService semesterService;
        private readonly List<Person> allStudents;

        public ObservableCollection<Person> Students { get; set; }
        public string Query { get; set; }

        public UnenrollStudentsViewModel()
        {
            personService = PersonService.Current;
            semesterService = SemesterService.Current;
            allStudents = new List<Person>();
            foreach(var person in FakeDataBase.People)
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
            foreach(var person in Students)
            {
                if(person.IsSelected == true)
                {
                    FakeDataBase.People.Remove(person);
                    allStudents.Remove(person);
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
