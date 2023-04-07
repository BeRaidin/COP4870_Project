using Library.LearningManagement.Services;
using Library.LearningManagement.Models;
using UWP.LearningManagement.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.UI.Xaml.Controls;
using Windows.Foundation.Collections;
using System.Reflection.Metadata;

namespace UWP.LearningManagement.ViewModels
{
    public class SelectPersonViewModel
    {
        private readonly PersonService personService;
        private readonly SemesterService semesterService;
        private readonly List<Person> allPeople;
        private readonly int personType;

        private ObservableCollection<Person> _people;
        public ObservableCollection<Person> People
        {
            get
            {
                return _people;
            }
            private set
            {
                _people = value;
            }
        }
        private ObservableCollection<Person> _instructors;
        public ObservableCollection<Person> Instructors
        {
            get { return _instructors; }
            private set { _instructors = value; }
        }
        private ObservableCollection<Person> _students;
        public ObservableCollection<Person> Students
        {
            get { return _students; }
            private set { _students = value; }
        }
        public Person SelectedPerson
        {
            get { return personService.CurrentPerson; }
            set { personService.CurrentPerson = value; }
        }
        public string Query { get; set; }

        public SelectPersonViewModel(int personType) 
        {
            personService = PersonService.Current;
            semesterService = SemesterService.Current;
            this.personType = personType;
            allPeople = semesterService.CurrentSemester.People;
            People = new ObservableCollection<Person>();
            Instructors = new ObservableCollection<Person>();
            Students = new ObservableCollection<Person>();
            foreach (Person person in allPeople) 
            { 
                if(person as Student == null)
                {
                    Instructors.Add(person);
                }
                else
                {
                    Students.Add(person);
                }
            }
            Refresh();
        }

        public void Search()
        {
            if (Query != null && Query != "")
            {

                IEnumerable<Person> searchResults;

                if(personType == 0)
                {
                    searchResults = Instructors.Where(i => i.Name.Contains(Query, StringComparison.InvariantCultureIgnoreCase)
                                                || i.Id.Contains(Query, StringComparison.InvariantCultureIgnoreCase));
                }
                else
                {
                    searchResults = Students.Where(i => i.Name.Contains(Query, StringComparison.InvariantCultureIgnoreCase)
                                               || i.Id.Contains(Query, StringComparison.InvariantCultureIgnoreCase));
                }
                
                People.Clear();
                foreach (var person in searchResults)
                {
                    People.Add(person);
                }
            }
            else
            {
                Refresh();
            }
        }

        public void Refresh()
        {
            People.Clear();
            if(personType == 0)
            {
                foreach(var person in Instructors)
                {
                    People.Add(person);
                }
            }
            else if (personType == 1)
            {
                foreach (var person in Students)
                {
                    People.Add(person);
                }
            }
        }
    }
}
