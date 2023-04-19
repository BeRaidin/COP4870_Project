using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWP.LearningManagement.API.Util;
using UWP.Library.LearningManagement.Models;

namespace UWP.LearningManagement.ViewModels
{
    public class TeachingAssistantViewModel : AdminViewModel
    {
        public new string Display
        {
            get { return $"[{Person.Id}] {Person.FirstName} {Person.LastName} - Teaching Assistant"; }
        }

        public TeachingAssistantViewModel(TeachingAssistant assistant)
        {
            Person = assistant;
        }

        public TeachingAssistantViewModel()
        {
            Person = new TeachingAssistant { Id = "-1" };
        }

        public async Task<TeachingAssistant> AddTeachingAssistant()
        {
            var handler = new WebRequestHandler();
            var returnVal = await handler.Post("http://localhost:5159/TeachingAssistant", Person);
            var deserializedReturn = JsonConvert.DeserializeObject<TeachingAssistant>(returnVal);
            return deserializedReturn;
        }
    }
}
