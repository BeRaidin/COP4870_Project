using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP.Library.LearningManagement.Models
{
    public class Student : Person
    {
        public List<GradesDictionary> Grades { get; set; }
        public Classes Classification { get; set; }
        public enum Classes
        {
            Freshman, Sophmore, Junior, Senior
        }
        public List<FinalGradesDictionary> FinalGrades { get; set; }
        public double GradePointAverage { get; set; }

        public override string Display => $"[{Id}] {FirstName} {LastName} - {Classification}";

        public Student()
        {
            Grades = new List<GradesDictionary>();
            FinalGrades = new List<FinalGradesDictionary>();
            IsSelected = false;
        }

        public void Remove(Assignment assignment)
        {
            foreach (var grade in Grades.ToList())
            {
                if (grade.Assignment.Equals(assignment))
                {
                    Grades.Remove(grade);
                }
            }
        }

        public GradesDictionary GetGradeDict(Assignment assignment)
        {
            foreach (var grade in Grades)
            {
                if (grade.Assignment.Equals(assignment))
                {
                    return grade;
                }
            }
            return null;
        }
    }
}
