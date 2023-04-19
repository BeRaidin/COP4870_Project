using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public ObservableCollection<string> AdminTypes
        {
            get
            {
                return new ObservableCollection<string> { "Instructor", "Teaching Assistant"};
            }
        }

        public string SelectedType { get; set; }

        public PersonViewModel(PersonDTO dto)
        {
            Dto = dto;
        }

        public PersonViewModel()
        {
            Dto = new PersonDTO { Id = "-1" };
        }

        public async Task<PersonDTO> AddAdmin()
        {
            var handler = new WebRequestHandler();
            string returnVal;


            PersonDTO deserializedReturn;
            if (SelectedType == "Instructor")
            {
                returnVal = await handler.Post("http://localhost:5159/Instructor", Dto);
                deserializedReturn = JsonConvert.DeserializeObject<InstructorDTO>(returnVal);
            }
            else
            {
                returnVal = await handler.Post("http://localhost:5159/TeachingAssistant", Dto);
                deserializedReturn = JsonConvert.DeserializeObject<TeachingAssistantDTO>(returnVal);
            }

            return deserializedReturn;
        }
    }
}
