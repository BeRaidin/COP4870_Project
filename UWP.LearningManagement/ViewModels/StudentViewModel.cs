using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWP.LearningManagement.API.Util;
using UWP.Library.LearningManagement.DTO;
using UWP.Library.LearningManagement.Models;
using Windows.Data.Json;

namespace UWP.LearningManagement.ViewModels
{
    public class StudentViewModel
    {
        public StudentDTO Dto { get; set; }

        public string Display
        {
            get { return $"[{Dto.Id}] {Dto.FirstName} {Dto.LastName} - {Dto.Classification}"; }
        }

        public StudentViewModel(StudentDTO dto)
        {
            Dto = dto;
        }

        public StudentViewModel()
        {
            Dto = new StudentDTO{ Id = "-1"};

        }

        public async Task<StudentDTO> Addstudent()
        {
            var handler = new WebRequestHandler();
            var returnVal = await handler.Post("http://localhost:5159/Student", Dto);
            var deserializedReturn = JsonConvert.DeserializeObject<StudentDTO>(returnVal);
            return deserializedReturn;
        }
        
    }
}
