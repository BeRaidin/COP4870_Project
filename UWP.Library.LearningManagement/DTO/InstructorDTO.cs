﻿using System;
using System.Collections.Generic;
using System.Text;
using UWP.Library.LearningManagement.Models;

namespace UWP.Library.LearningManagement.DTO
{
    public class InstructorDTO : PersonDTO
    {
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
