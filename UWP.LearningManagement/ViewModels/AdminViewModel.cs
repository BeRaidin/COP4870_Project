using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWP.LearningManagement.API.Util;
using UWP.Library.LearningManagement.Models;

namespace UWP.LearningManagement.ViewModels
{
    public class AdminViewModel
    {
        public InstructorViewViewModel ParentViewModel;
        public Person Person { get; set; }

        public string Display
        {
            get { return $"[{Person.Id}] {Person.FirstName} {Person.LastName}"; }
        }

        public ObservableCollection<string> AdminTypes
        {
            get
            {
                return new ObservableCollection<string> { "Instructor", "Teaching Assistant"};
            }
        }

        public string SelectedType { get; set; }

        public AdminViewModel(InstructorViewViewModel ivm)
        {
            ParentViewModel = ivm;
            if (ParentViewModel?.SelectedInstructor?.Person == null)
            {
                Person = new Person { Id = "-1" };
            }
            else Person = ParentViewModel.SelectedInstructor.Person;
        }

        public AdminViewModel() { }

        public async Task<Person> AddAdmin()
        {
            var handler = new WebRequestHandler();
            string returnVal;


            Person deserializedReturn;
            if (SelectedType == "Instructor")
            {
                returnVal = await handler.Post("http://localhost:5159/Instructor/AddOrUpdate", Person);
                deserializedReturn = JsonConvert.DeserializeObject<Instructor>(returnVal);
            }
            else
            {
                returnVal = await handler.Post("http://localhost:5159/TeachingAssistant/AddOrUpdate", Person);
                deserializedReturn = JsonConvert.DeserializeObject<TeachingAssistant>(returnVal);
            }

            return deserializedReturn;
        }
    }
}
