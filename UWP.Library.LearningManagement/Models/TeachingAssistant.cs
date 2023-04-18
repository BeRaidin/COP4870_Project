using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWP.Library.LearningManagement.DTO;

namespace UWP.Library.LearningManagement.Models
{
    public class TeachingAssistant : Person
    {
        public override string Display => $"[{Id}] {FirstName} {LastName}  - Teaching Assistant";

        public TeachingAssistant() { }

        public TeachingAssistant(TeachingAssistantDTO dto)
        {
            Id = dto.Id;
            FirstName = dto.FirstName;
            LastName = dto.LastName;
            Courses = dto.Courses;
            IsSelected = false;
        }
    }
}
