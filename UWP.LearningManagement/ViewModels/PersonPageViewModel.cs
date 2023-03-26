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
        private List<Person> allPeople;
        private ObservableCollection<Person> people;
        public string Query { get; set; }
        public Person SelectedPerson { get; set; }
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

        public PersonPageViewModel()
        {
            personService = new PersonService();
            allPeople = personService.People;
            People = new ObservableCollection<Person>(allPeople);
        }

        public async void Add() 
        {
            var dialog = new PersonDialog(allPeople);
            if(dialog != null)
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
        }

        public void Remove()
        {
            People.Remove(SelectedPerson);
        }
    }
}
