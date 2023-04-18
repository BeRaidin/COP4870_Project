﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWP.Library.LearningManagement.DTO;

namespace UWP.Library.LearningManagement.Models
{
    public class Instructor : Person
    {
        public override string Display => $"[{Id}] {FirstName} {LastName}  - Instructor";

        public Instructor() { }

        public Instructor(InstructorDTO dto) 
        { 
            Id = dto.Id ;
            FirstName = dto.FirstName;
            LastName = dto.LastName;
            Courses = dto.Courses;
            IsSelected = false;
        }
    }
}
