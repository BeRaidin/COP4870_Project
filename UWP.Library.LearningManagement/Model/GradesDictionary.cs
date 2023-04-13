using Library.LearningManagement.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace Library.LearningManagement.Model
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
        public bool isGraded { get; set; }
        public bool isSubmitted { get; set; }

        public GradesDictionary()
        {
            Course = new Course();
            Assignment = new Assignment();
            isGraded = false;
            isSubmitted = false;
            Grade = 0;
        }



        public virtual string Display => $"[{Course.Code}] {Assignment.Name} - {Grade}/{Assignment.TotalAvailablePoints}";
        public virtual string DueDateDisplay => $"[{Course.Code}] {Assignment.Name}" +
                                                $"\nDue: {Assignment.DueDate}";
        public virtual string PersonDisplay => $"[{Course.Code}] {Person.FirstName} {Person.LastName} - {Assignment.Name}";
    }

}
