using System;
using System.Collections.Generic;
using System.Text;
using UWP.Library.LearningManagement.Models;

namespace UWP.Library.LearningManagement.DTO
{
    public class InstructorDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Course> Courses { get; set; }
        public bool IsSelected { get; set; }

        public InstructorDTO()
        {
        }

        public InstructorDTO(Person person)
        {
            Id = person.Id;
            FirstName = person.FirstName;
            LastName = person.LastName;
            Courses = person.Courses;
            IsSelected = false;
        }
    }
}
