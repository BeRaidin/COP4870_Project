using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UWP.LearningManagement.Dialogs;
using UWP.Library.LearningManagement.Database;

namespace UWP.LearningManagement.ViewModels
{
    public class InstructorViewViewModel
    {
        private readonly PersonService personService;
        private readonly SemesterService semesterService;
        private List<Person> AllInstructors { get; set; }

        public ObservableCollection<Person> Instructors { get; set; }
        public Person SelectedPerson
        {
            get { return personService.CurrentPerson; }
            set { personService.CurrentPerson = value; }
        }
        public Semester SelectedSemester
        {
            get { return semesterService.CurrentSemester; }
        }
        public string Semester
        {
            get { return semesterService.CurrentSemester.Period; }
        }
        public int Year
        {
            get { return semesterService.CurrentSemester.Year; }
        }
        public string Query { get; set; }

        public InstructorViewViewModel()
        {
            personService = PersonService.Current;
            semesterService = SemesterService.Current;
            AllInstructors = new List<Person>();
            foreach (var person in SelectedSemester.People)
            {
                if (person as Student == null)
                {
                    AllInstructors.Add(person);
                }
            }
            Instructors = new ObservableCollection<Person>(AllInstructors);
        }

        public void Search()
        {
            if (Query != null && Query != "")
            {

                IEnumerable<Person> searchResults = AllInstructors.Where(i => i.FirstName.Contains(Query, StringComparison.InvariantCultureIgnoreCase)
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
            if(!dialog.TestValid())
            {
                var errorDialog = new ErrorDialog();
                if (errorDialog != null)
                {
                    await errorDialog.ShowAsync();
                }
            }
            AllInstructors.Clear();
            Instructors.Clear();
            foreach (var person in SelectedSemester.People)
            {
                if (person as Student == null)
                {
                    AllInstructors.Add(person);
                    Instructors.Add(person);
                }
            }
        }

        public void Refresh()
        {
            Instructors.Clear();
            foreach (var person in AllInstructors)
            {
                Instructors.Add(person);
            }
        }
    }
}
