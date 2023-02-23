﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Dictionary<int, double> Grades { get; set; }
        public Classes Classification { get; set; }

        public enum Classes
        {
            Freshman, Sophmore, Junior, Senior
        }

        
        public Person() {
            Name = string.Empty;
            Grades = new Dictionary<int, double>();
        }

        public override string ToString()
        {
            return $"[{Id}] {Name} - {Classification}";
        }
    }
}