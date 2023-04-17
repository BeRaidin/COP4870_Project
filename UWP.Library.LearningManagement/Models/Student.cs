using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWP.Library.LearningManagement.DTO;

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
        public Dictionary<Course, double> FinalGrades { get; set; }
        public double GradePointAverage { get; set; }

        public override string Display => $"[{Id}] {FirstName} {LastName} - Student";

        public Student()
        {
            Grades = new List<GradesDictionary>();
            FinalGrades = new Dictionary<Course, double>();
        }
        public Student(StudentDTO dto)
        {
            Id = dto.Id;
            FirstName = dto.FirstName;
            LastName = dto.LastName;
            Courses = dto.Courses;
            if (dto.Classification == StudentDTO.Classes.Freshman)
            {
                Classification = Classes.Freshman;
            }
            else if (dto.Classification == StudentDTO.Classes.Sophmore)
            {
                Classification = Classes.Sophmore;
            }
            else if (dto.Classification == StudentDTO.Classes.Junior)
            {
                Classification = Classes.Junior;
            }
            else
            {
                Classification = Classes.Senior;
            }
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
