using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWP.LearningManagement.Dialogs;
using UWP.Library.LearningManagement.Database;

namespace UWP.LearningManagement.ViewModels
{
    public class InstructorViewViewModel
    {
        private readonly PersonService personService;
        private readonly SemesterService semesterService;
        private readonly List<Person> allInstructors;

        public ObservableCollection<Person> Instructors { get; set; }
        public Person SelectedPerson
        {
            get { return personService.CurrentPerson; }
            set { personService.CurrentPerson = value; }
        }

        public string Semester { get { return semesterService.CurrentSemester.Period; } }
        public int Year { get { return semesterService.CurrentSemester.Year; } }
        public string Query { get; set; }

        public InstructorViewViewModel()
        {
            personService = PersonService.Current;
            semesterService = SemesterService.Current;
            allInstructors = new List<Person>();
            foreach(var person in FakeDataBase.People)
            {
                if (person as Student == null)
                {
                    allInstructors.Add(person);
                }
            }
            Instructors = new ObservableCollection<Person>(allInstructors);
        }

        public void Search()
        {
            if (Query != null && Query != "")
            {

                IEnumerable<Person> searchResults = Instructors.Where(i => i.FirstName.Contains(Query, StringComparison.InvariantCultureIgnoreCase)
                                                    || i.Id.Contains(Query, StringComparison.InvariantCultureIgnoreCase));
                Instructors.Clear();
                foreach (var person in searchResults)
                {
                    Instructors.Add(person);
                }
            }
            else
            {
                Refresh();
            }
        }

        public async void Add()
        {
            SelectedPerson = new Person();
            var dialog = new PersonDialog();
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            Refresh();
        }

        public void Refresh()
        {
            Instructors.Clear();
            foreach(var instructor in allInstructors)
            {
                Instructors.Add(instructor);
            }
        }
    }
}
