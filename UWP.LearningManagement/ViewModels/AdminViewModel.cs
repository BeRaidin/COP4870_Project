using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWP.LearningManagement.API.Util;
using UWP.Library.LearningManagement.Database;
using UWP.Library.LearningManagement.Models;
using Windows.UI.Xaml;

namespace UWP.LearningManagement.ViewModels
{
    public class AdminViewModel
    {
        public IEnumerable<Instructor> Instructors
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Person/GetInstructors").Result;
                return JsonConvert.DeserializeObject<List<Instructor>>(payload);
            }
        }

        public IEnumerable<TeachingAssistant> Assistants
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Person/GetAssistants").Result;
                return JsonConvert.DeserializeObject<List<TeachingAssistant>>(payload);
            }
        }


        public Person Person { get; set; }

        public string Display
        {
            get { return $"[{Person.Id}] {Person.FirstName} {Person.LastName} - {SelectedType}"; }
        }

        public ObservableCollection<string> AdminTypes
        {
            get
            {
                return new ObservableCollection<string> { "Instructor", "Teaching Assistant" };
            }
        }

        public string SelectedType { get; set; }

        public AdminViewModel(int id)
        {
            if (id != -1)
            {

                Person = Instructors.FirstOrDefault(x => x.Id == id);
                if (Person == null)
                {
                    Person = Assistants.FirstOrDefault(x => x.Id == id);
                    SelectedType = "Teaching Assistant";
                }
                else SelectedType = "Instructor";

            }
            else Person = new Person { Id = -1 };
        }

        public AdminViewModel() { }

        public async Task<Person> AddAdmin()
        {
            var handler = new WebRequestHandler();
            string returnVal;


            Person deserializedReturn;
            if (SelectedType == "Instructor")
            {
                returnVal = await handler.Post("http://localhost:5159/Person/AddOrUpdateInstructor", Person);
                deserializedReturn = JsonConvert.DeserializeObject<Instructor>(returnVal);
            }
            else
            {
                returnVal = await handler.Post("http://localhost:5159/Person/AddOrUpdateAssistant", Person);
                deserializedReturn = JsonConvert.DeserializeObject<TeachingAssistant>(returnVal);
            }

            return deserializedReturn;
        }
    }
}
