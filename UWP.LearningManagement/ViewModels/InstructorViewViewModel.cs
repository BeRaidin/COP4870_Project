using UWP.Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UWP.LearningManagement.Dialogs;
using UWP.Library.LearningManagement.Database;
using Newtonsoft.Json;
using UWP.LearningManagement.API.Util;
using UWP.Library.LearningManagement.DTO;

namespace UWP.LearningManagement.ViewModels
{
    public class InstructorViewViewModel
    {
        private readonly PersonService personService;
        private readonly SemesterService semesterService;


        public IEnumerable<PersonViewModel> AllInstructors
        {
            get
            {
                var payloadInstructors = new WebRequestHandler().Get("http://localhost:5159/Instructor").Result;
                var payloadAssistants = new WebRequestHandler().Get("http://localhost:5159/TeachingAssistant").Result;
                List<InstructorViewModel> instructorsList = JsonConvert.DeserializeObject<List<InstructorDTO>>(payloadInstructors).Select(d => new InstructorViewModel(d)).ToList();
                List<TeachingAssistantViewModel> assistantsList = JsonConvert.DeserializeObject<List<TeachingAssistantDTO>>(payloadAssistants).Select(d => new TeachingAssistantViewModel(d)).ToList();
                List<PersonViewModel> results = new List<PersonViewModel>();
                foreach(var instructor in  instructorsList)
                {
                    results.Add(instructor);
                }
                foreach (var TeachingAssistant in assistantsList)
                {
                    results.Add(TeachingAssistant);
                }

                IEnumerable<PersonViewModel> returnVal = results;

                return returnVal;
            }
        }

        public ObservableCollection<PersonViewModel> Instructors { get; set; }
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
            Instructors = new ObservableCollection<PersonViewModel>(AllInstructors);
        }

        public void Search()
        {
            if (Query != null && Query != "")
            {

                IEnumerable<PersonViewModel> searchResults = AllInstructors.Where(i => i.Dto.FirstName.Contains(Query, StringComparison.InvariantCultureIgnoreCase)
                                                    || i.Dto.Id.Contains(Query, StringComparison.InvariantCultureIgnoreCase));
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
            Refresh();
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
