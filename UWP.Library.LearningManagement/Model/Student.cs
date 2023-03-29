using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Models
{
    public class Student : Person
    {
        public Dictionary<Assignment, double> Grades { get; set; }
        public Classes Classification { get; set; }
        public enum Classes
        {
            Freshman, Sophmore, Junior, Senior
        }
        public Dictionary<Course, double> FinalGrades { get; set; }

        public double GradePointAverage { get; set; }

        public Student()
        {
            Grades = new Dictionary<Assignment, double>();
            FinalGrades = new Dictionary<Course, double>();
        }

        public void DeleteGrades()
        {
            Grades.Clear();
            Grades = new Dictionary<Assignment, double>();
        }
    }
}
