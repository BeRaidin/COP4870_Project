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
    public class InstructorViewModel
    {
        public InstructorDTO Dto { get; set; }

        public string Display
        {
            get { return $"[{Dto.Id}] {Dto.FirstName} {Dto.LastName}"; }
        }

        public InstructorViewModel(InstructorDTO dto)
        {
            Dto = dto;
        }

        public InstructorViewModel()
        {
            Dto = new InstructorDTO { Id = "-1" };

        }

        public async Task<InstructorDTO> Addstudent()
        {
            var handler = new WebRequestHandler();
            var returnVal = await handler.Post("http://localhost:5159/Instructor", Dto);
            var deserializedReturn = JsonConvert.DeserializeObject<InstructorDTO>(returnVal);
            return deserializedReturn;
        }
    }
}
