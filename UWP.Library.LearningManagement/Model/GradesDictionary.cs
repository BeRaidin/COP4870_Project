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

        public GradesDictionary() 
        {
            Assignment = new Assignment();
            Grade = 0;
        }

        public virtual string Display => $"{Assignment.Name} - {Grade}/{Assignment.TotalAvailablePoints}" +
                                         $"\nDue: {Assignment.DueDate}";
    }
}
