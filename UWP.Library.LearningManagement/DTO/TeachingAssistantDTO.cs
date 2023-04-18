using System;
using System.Collections.Generic;
using System.Text;
using UWP.Library.LearningManagement.Models;

namespace UWP.Library.LearningManagement.DTO
{
    public class TeachingAssistantDTO : PersonDTO
    {
        public TeachingAssistantDTO()
        {
        }

        public TeachingAssistantDTO(Person person)
        {
            Id = person.Id;
            FirstName = person.FirstName;
            LastName = person.LastName;
            Courses = person.Courses;
            IsSelected = false;
        }
    }
}
