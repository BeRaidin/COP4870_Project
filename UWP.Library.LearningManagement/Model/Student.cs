using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public void Add(Course course) 
        {
            Courses.Add(course);
            foreach(var assignment in course.Assignments) 
            {
                Grades.Add(assignment, 0);
            }
            FinalGrades.Add(course, 0);
        }

        public void Remove(Course course) 
        { 
            Courses.Remove(course);
            foreach (var assignment in course.Assignments)
            {
                if(Grades.ContainsKey(assignment))
                {
                    Grades.Remove(assignment);
                }
            }
            FinalGrades.Remove(course);
        }
    }
}
