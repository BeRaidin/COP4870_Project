using System.Collections.Generic;
using UWP.Library.LearningManagement.Models;

namespace UWP.Library.LearningManagement.DTO
{
    public class StudentDTO : PersonDTO
    {
        public List<GradesDictionary> Grades { get; set; }
        public Classes Classification { get; set; }
        public enum Classes
        {
            Freshman, Sophmore, Junior, Senior
        }
        public Dictionary<Course, double> FinalGrades { get; set; }
        public double GradePointAverage { get; set; }

        public StudentDTO(Student student)
        {
            Id = student.Id;
            FirstName = student.FirstName;
            LastName = student.LastName;
            Courses = student.Courses;
            if (student.Classification == Student.Classes.Freshman)
            {
                Classification = Classes.Freshman;
            }
            else if (student.Classification == Student.Classes.Sophmore)
            {
                Classification = Classes.Sophmore;
            }
            else if (student.Classification == Student.Classes.Junior)
            {
                Classification = Classes.Junior;
            }
            else
            {
                Classification = Classes.Senior;
            }
        }

        public StudentDTO()
        {
        }
    }
}
