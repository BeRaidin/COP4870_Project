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
        private IEnumerable<Person> AllInstructors
        {
            get
            {
                var payloadInstructors = new WebRequestHandler().Get("http://localhost:5159/Person/GetInstructors").Result;
                var payloadAssistants = new WebRequestHandler().Get("http://localhost:5159/Person/GetAssistants").Result;
                List<Instructor> instructorsList = JsonConvert.DeserializeObject<List<Instructor>>(payloadInstructors).ToList();
                List<TeachingAssistant> assistantsList = JsonConvert.DeserializeObject<List<TeachingAssistant>>(payloadAssistants).ToList();
                List<Person> results = new List<Person>();
                foreach(var instructor in  instructorsList)
                {
                    results.Add(instructor);
                }
                foreach (var teachingAssistant in assistantsList)
                {
                    results.Add(teachingAssistant);
                }

                IEnumerable<Person> returnVal = results;

                return returnVal;
            }
        }







        public ObservableCollection<Person> Instructors { get; set; }
        public Person SelectedInstructor { get; set; }
        
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
            Instructors = new ObservableCollection<Person>(AllInstructors);
        }

        public void Search()
        {
            if (Query != null && Query != "")
            {
                IEnumerable<Person> searchResults;
                if (int.TryParse(Query, out int id))
                {
                    searchResults = AllInstructors.Where(i => i.Id == id).ToList();
                }
                else
                {
                    searchResults = AllInstructors.Where(i => i.FirstName.Contains(Query, StringComparison.InvariantCultureIgnoreCase));
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
            string returnVal = await new WebRequestHandler().Post("http://localhost:5159/Instructor/Delete", SelectedInstructor);
            var deserializedReturn = JsonConvert.DeserializeObject<Instructor>(returnVal);
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
