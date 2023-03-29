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
        private readonly List<Person> allPeople;
        private int PersonType;
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

        public Person SelectedPerson { get; set; }
        public string Query { get; set; }

        public SelectPersonViewModel(int personType) 
        {
            PersonType = personType;
            personService = PersonService.Current;
            allPeople = personService.PersonList;
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
            if (Query != null)
            {
                var searchResults = People.Where(i => i.Name.Contains(Query, StringComparison.InvariantCultureIgnoreCase));
                People.Clear();
                foreach (var item in searchResults)
                {
                    People.Add(item);
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
            if(PersonType == 0)
            {
                People = Instructors;
            }
            else if (PersonType == 1)
            {
                People = Students;
            }
        }

        public void UpdateSelectedPerson()
        {
            personService.CurrentPerson = SelectedPerson;
        }
    }
}
