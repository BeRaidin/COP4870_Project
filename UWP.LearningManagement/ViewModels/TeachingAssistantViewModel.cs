using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWP.LearningManagement.API.Util;
using UWP.Library.LearningManagement.DTO;

namespace UWP.LearningManagement.ViewModels
{
    public class TeachingAssistantViewModel : AdminViewModel
    {
        public new string Display
        {
            get { return $"[{Dto.Id}] {Dto.FirstName} {Dto.LastName} - Teaching Assistant"; }
        }

        public TeachingAssistantViewModel(TeachingAssistantDTO dto)
        {
            Dto = dto;
        }

        public TeachingAssistantViewModel()
        {
            Dto = new TeachingAssistantDTO { Id = "-1" };

        }

        public async Task<TeachingAssistantDTO> AddTeachingAssistant()
        {
            var handler = new WebRequestHandler();
            var returnVal = await handler.Post("http://localhost:5159/TeachingAssistant", Dto);
            var deserializedReturn = JsonConvert.DeserializeObject<TeachingAssistantDTO>(returnVal);
            return deserializedReturn;
        }
    }
}
