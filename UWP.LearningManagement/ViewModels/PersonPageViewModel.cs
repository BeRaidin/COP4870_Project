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
        private readonly SemesterService semesterService;
        private readonly ListNavigator<Person> allPeople;
        private bool isSearch;

        public Person SelectedPerson
        {
            get { return personService.CurrentPerson; }
            set { personService.CurrentPerson = value; }
        }
        public ObservableCollection<Person> People { get; set; }
        public string Query { get; set; }
        public ListNavigator<Person> SearchResults { get; set; }

        public PersonPageViewModel()
        {
            personService = PersonService.Current;
            semesterService = SemesterService.Current;
            allPeople = new ListNavigator<Person>(semesterService.CurrentSemester.People);
            People = new ObservableCollection<Person>(allPeople.PrintPage(allPeople.GoToFirstPage()));
            isSearch = false;
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
                semesterService.Remove(SelectedPerson);
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
            if (Query != null && Query != "")
            {
                isSearch = true;
                var search = allPeople.State.Where(i => i.Name.Contains(Query, StringComparison.InvariantCultureIgnoreCase) 
                                                    || i.Id.Contains(Query, StringComparison.InvariantCultureIgnoreCase));
                SearchResults = new ListNavigator<Person>(search.ToList());
                People.Clear();
                foreach (var item in SearchResults.PrintPage(SearchResults.GoToFirstPage()))
                {
                    People.Add(item);
                }
            }
            else
            {
                isSearch = false;
                Refresh();
            }
        }

        public void Refresh()
        {
            People.Clear();
            foreach (var person in allPeople.PrintPage(allPeople.GoToFirstPage()))
            {
                People.Add(person);
            }
        }

        public void LeftClick()
        {
            if (isSearch && SearchResults.HasPreviousPage)
            {
                People.Clear();
                foreach (var course in SearchResults.PrintPage(SearchResults.GoBackward()))
                {
                    People.Add(course);
                }
            }
            else if (!isSearch && allPeople.HasPreviousPage)
            {
                People.Clear();
                foreach (var course in allPeople.PrintPage(allPeople.GoBackward()))
                {
                    People.Add(course);
                }
            }
        }

        public void RightClick()
        {
            if (isSearch && SearchResults.HasNextPage)
            {
                People.Clear();
                foreach (var course in SearchResults.PrintPage(SearchResults.GoForward()))
                {
                    People.Add(course);
                }
            }
            else if (!isSearch && allPeople.HasNextPage)
            {
                People.Clear();
                foreach (var course in allPeople.PrintPage(allPeople.GoForward()))
                {
                    People.Add(course);
                }
            }
        }
    }
}