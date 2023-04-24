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
using System.Threading.Tasks;

namespace UWP.LearningManagement.ViewModels
{
    public class InstructorViewViewModel
    {
        private readonly SemesterService semesterService;
        private IEnumerable<AdminViewModel> AllInstructors
        {
            get
            {
                var payloadInstructors = new WebRequestHandler().Get("http://localhost:5159/Person/GetInstructors").Result;
                var payloadAssistants = new WebRequestHandler().Get("http://localhost:5159/Person/GetAssistants").Result;
                var instructorsList = JsonConvert.DeserializeObject<List<Instructor>>(payloadInstructors).Select(d => new AdminViewModel(d.Id));
                var assistantsList = JsonConvert.DeserializeObject<List<TeachingAssistant>>(payloadAssistants).Select(d => new AdminViewModel(d.Id));
                List<AdminViewModel> results = new List<AdminViewModel>();
                foreach (var instructor in instructorsList)
                {
                    results.Add(instructor);
                }
                foreach (var teachingAssistant in assistantsList)
                {
                    results.Add(teachingAssistant);
                }

                IEnumerable<AdminViewModel> returnVal = results;

                return returnVal;
            }
        }

        public ObservableCollection<AdminViewModel> Instructors { get; set; }
        public AdminViewModel SelectedInstructor { get; set; }

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
            semesterService = SemesterService.Current;
            Instructors = new ObservableCollection<AdminViewModel>(AllInstructors);
        }

        public void Search()
        {
            if (Query != null && Query != "")
            {
                IEnumerable<AdminViewModel> searchResults;
                if (int.TryParse(Query, out int id))
                {
                    searchResults = AllInstructors.Where(i => i.Person.Id == id).ToList();
                }
                else
                {
                    searchResults = AllInstructors.Where(i => i.Person.FirstName.Contains(Query, StringComparison.InvariantCultureIgnoreCase));
                }


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

        public async Task<Person> Delete()
        {
            string returnVal = await new WebRequestHandler().Post("http://localhost:5159/Person/Delete", SelectedInstructor.Person);
            var deserializedReturn = JsonConvert.DeserializeObject<Person>(returnVal);
            return deserializedReturn;
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
