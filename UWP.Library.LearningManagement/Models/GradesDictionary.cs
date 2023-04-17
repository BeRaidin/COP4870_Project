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
        private Assignment _assignment;
        public Assignment Assignment
        {
            get { return _assignment; }
            set { _assignment = value; }
        }
        private double _grade;
        public double Grade
        {
            get { return _grade; }
            set { _grade = value; }
        }

        public Course Course { get; set; }
        public Person Person { get; set; }
        public bool IsGraded { get; set; }
        public bool IsSubmitted { get; set; }

        public GradesDictionary()
        {
            Course = new Course();
            Assignment = new Assignment();
            IsGraded = false;
            IsSubmitted = false;
            Grade = 0;
        }



        public virtual string Display => $"[{Course.Code}] {Assignment.Name} - {Grade}/{Assignment.TotalAvailablePoints}";
        public virtual string DueDateDisplay => $"[{Course.Code}] {Assignment.Name}" +
                                                $"\nDue: {Assignment.DueDate}";
        public virtual string PersonDisplay => $"[{Course.Code}] {Person.FirstName} {Person.LastName} - {Assignment.Name}";
    }

}
