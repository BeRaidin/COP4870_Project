using System;
using System.Collections.Generic;
using System.Text;

namespace UWP.Library.LearningManagement.Models
{
    public class FinalGradesDictionary
    {
        public Course Key { get; set; }
        public double Value { get; set; }
        public FinalGradesDictionary()
        {
            Key = new Course();
            Value = 0;
        }

        public FinalGradesDictionary(Course course, double grade)
        {
            Key = course;
            Value = grade;
        }
    }

}
