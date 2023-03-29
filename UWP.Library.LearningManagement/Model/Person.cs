﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Models
{
    public class Person
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public List<Course> Courses { get; set; }
        public bool IsSelected { get; set; }

        public Person() {
            Name = string.Empty;
            Courses = new List<Course>();
            IsSelected = false;
        }

        public virtual string Display => $"[{Id}] {Name}";

        public void AddCourse(Course course)
        {
            Courses.Add(course);
        }
    }
}