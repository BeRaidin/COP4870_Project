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

namespace UWP.LearningManagement.ViewModels
{
    public class PersonPageViewModel
    {
        private PersonService personService;
        private CourseService courseService;
        private List<Person> allPeople;
        private ObservableCollection<Person> people;
        public ObservableCollection<Person> People
        {
            get
            {
                return people;
            }
            private set
            {
                people = value;
            }
        }
        public Person SelectedPerson { get; set; }

        public string Query { get; set; }

        public PersonPageViewModel()
        {
            personService = PersonService.Current;
            courseService = CourseService.Current;
            allPeople = personService.People;
            People = new ObservableCollection<Person>(allPeople);
        }

        public async void Add()
        {
            var dialog = new PersonDialog();
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
        }

        public void Search()
        {
            if (Query != null)
            {
                var searchResults = allPeople.Where(i => i.Name.Contains(Query));
                People.Clear();
                foreach (var item in searchResults)
                {
                    People.Add(item);
                }
            }
            else
            {
                People.Clear();
                foreach (var person in allPeople)
                {
                    People.Add(person);
                }
            }
        }

        public void Remove()
        {
            UpdateCurrentPerson();
            personService.Remove();
            people.Remove(SelectedPerson);
        }

        public async void Edit()
        {
            UpdateCurrentPerson();
            if (SelectedPerson != null)
            {
                var dialog = new EditPersonDialog();
                if (dialog != null)
                {
                    await dialog.ShowAsync();
                }
            }
        }

        public void UpdateCurrentPerson()
        {
            personService.CurrentPerson = SelectedPerson;
        }
    }
}
