using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWP.Library.LearningManagement.DTO;

namespace UWP.LearningManagement.ViewModels
{
    public class StudentViewModel
    {
        public StudentDTO Dto { get; set; }

        public string Display
        {
            get { return $"[{Dto.Id}] {Dto.FirstName} {Dto.LastName} - {Dto.Classification}"; }
        }

        public StudentViewModel (StudentDTO dto)
        {
            Dto = dto;
        }   
    }
}
