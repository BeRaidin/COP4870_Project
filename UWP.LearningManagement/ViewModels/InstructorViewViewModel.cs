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
using Windows.Devices.Bluetooth.Advertisement;

namespace UWP.LearningManagement.ViewModels
{
    public class InstructorViewViewModel
    {
        private readonly PersonService personService;
        private readonly SemesterService semesterService;


        public IEnumerable<AdminViewModel> AllInstructors
        {
            get
            {
                var payloadInstructors = new WebRequestHandler().Get("http://localhost:5159/Instructor").Result;
                var payloadAssistants = new WebRequestHandler().Get("http://localhost:5159/TeachingAssistant").Result;
                List<InstructorViewModel> instructorsList = JsonConvert.DeserializeObject<List<Instructor>>(payloadInstructors).Select(d => new InstructorViewModel(this, d)).ToList();
                List<TeachingAssistantViewModel> assistantsList = JsonConvert.DeserializeObject<List<TeachingAssistant>>(payloadAssistants).Select(d => new TeachingAssistantViewModel(this, d)).ToList();
                List<AdminViewModel> results = new List<AdminViewModel>();
                foreach(var instructor in  instructorsList)
                {
                    results.Add(instructor);
                }
                foreach (var TeachingAssistant in assistantsList)
                {
                    results.Add(TeachingAssistant);
                }

                IEnumerable<AdminViewModel> returnVal = results;

                return returnVal;
            }
        }

        public ObservableCollection<AdminViewModel> Instructors { get; set; }
        public AdminViewModel SelectedInstructor { get; set; }
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
            Instructors = new ObservableCollection<AdminViewModel>(AllInstructors);
        }

        public void Search()
        {
            if (Query != null && Query != "")
            {

                IEnumerable<AdminViewModel> searchResults = AllInstructors.Where(i => i.Person.FirstName.Contains(Query, StringComparison.InvariantCultureIgnoreCase)
                                                    || i.Person.Id.Contains(Query, StringComparison.InvariantCultureIgnoreCase));
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
