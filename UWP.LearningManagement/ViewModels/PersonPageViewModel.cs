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
    public class PersonPageViewModel
    {
        private readonly PersonService personService;
        private readonly List<Person> allPeople;

        public Person SelectedPerson
        {
            get { return personService.CurrentPerson; }
            set { personService.CurrentPerson = value; }
        }
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
        public string Query { get; set; }

        public PersonPageViewModel()
        {
            personService = PersonService.Current;
            allPeople = personService.PersonList;
            People = new ObservableCollection<Person>(allPeople);
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

        public void Remove()
        {
            if (SelectedPerson != null)
            {
                personService.Remove();
                Refresh();
            }
        }

        public async void Edit()
        {
            if (SelectedPerson != null)
            {
                var dialog = new EditPersonDialog();
                if (dialog != null)
                {
                    await dialog.ShowAsync();
                }
            }
            Refresh();
        }

        public void Search()
        {
            if (Query != null)
            {
                var searchResults = allPeople.Where(i => i.Name.Contains(Query, StringComparison.InvariantCultureIgnoreCase)
                                                || i.Id.Contains(Query, StringComparison.InvariantCultureIgnoreCase));
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
            foreach (var person in allPeople)
            {
                People.Add(person);
            }
        }
    }
}
