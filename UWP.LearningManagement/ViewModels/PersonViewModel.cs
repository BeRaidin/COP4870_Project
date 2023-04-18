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
    public class PersonViewModel
    {
        public PersonDTO Dto { get; set; }

        public string Display
        {
            get { return $"[{Dto.Id}] {Dto.FirstName} {Dto.LastName}"; }
        }

        public PersonViewModel(PersonDTO dto)
        {
            Dto = dto;
        }

        public PersonViewModel()
        {
            Dto = new PersonDTO { Id = "-1" };
        }

        public async Task<PersonDTO> AddInstructor()
        {
            var handler = new WebRequestHandler();
            var returnVal = await handler.Post("http://localhost:5159/Instructor", Dto);
            var deserializedReturn = JsonConvert.DeserializeObject<PersonDTO>(returnVal);
            return deserializedReturn;
        }
    }
}
