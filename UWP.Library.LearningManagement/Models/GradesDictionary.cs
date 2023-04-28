using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace UWP.Library.LearningManagement.Models
{
    public class GradesDictionary
    {
        public Assignment Assignment { get; set; }
        public double Grade { get; set; }
        public string CourseCode { get; set; }
        public string PersonName { get; set; }
        public int PersonId { get; set; }
        public int CourseId { get; set; }

        public GradesDictionary()
        {
            Assignment = new Assignment();
            Grade = 0;
        }

        public GradesDictionary(Assignment assignment, Course course, Student student, double score = 0)
        {
            Assignment = assignment;
            PersonName = student.FirstName + " " + student.LastName;
            CourseCode = course.Code;
            PersonId = student.Id;
            CourseId = course.Id;
            Grade = score;
        }




        public virtual string Display => $"[{CourseCode}] {Assignment.Name} - {Grade}/{Assignment.TotalAvailablePoints}";
        public virtual string DueDateDisplay => $"[{CourseCode}] {Assignment.Name}" +
                                                $"\nDue: {Assignment.DueDate}";
        public virtual string PersonDisplay => $"[{CourseCode}] {PersonName} - {Assignment.Name}";
    }

}
