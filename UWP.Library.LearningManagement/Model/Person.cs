using Library.LearningManagement.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Models
{
    public class Person
    {
        public string Id {  get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<Course> Courses { get; set; }
        public bool IsSelected { get; set; }

        public Person() {
            FirstName = string.Empty;
            LastName = string.Empty;
            Courses = new List<Course>();
            IsSelected = false;
        }

        public virtual string Display => $"[{Id}] {FirstName} {LastName}";

        public void Add(Course course)
        {
            Courses.Add(course);
            if(this as Student != null)
            {
                foreach (var assignment in course.Assignments)
                {
                    var grade = new GradesDictionary { Assignment = assignment, Grade = 0 };


                    (this as Student).Grades.Add(grade);
                }
                (this as Student).FinalGrades.Add(course, 0);
            }
        }

        public void Remove(Course course)
        {
            Courses.Remove(course);
            if (this as Student != null)
            {
                foreach (var assignment in course.Assignments)
                {
                    (this as Student).Remove(assignment);
                }
                (this as Student).FinalGrades.Remove(course);
            }
        }
    }
}