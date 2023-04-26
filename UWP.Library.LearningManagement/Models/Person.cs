using System.Collections.Generic;
using System.Linq;

namespace UWP.Library.LearningManagement.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Course> Courses { get; set; }
        public bool IsSelected { get; set; }

        public Person()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Courses = new List<Course>();
            IsSelected = false;
        }
        


        public virtual string Display => $"[{Id}] {FirstName} {LastName}";

        public void Add(Course course)
        {
            Courses.Add(course);
            if (this is Student student)
            {
                foreach (var assignment in course.Assignments)
                {
                    var grade = new GradesDictionary { Assignment = assignment, Grade = 0, Course = course, Person = this };
                    student.Grades.Add(grade);
                }
                student.FinalGrades.Add(new FinalGradesDictionary(course, 0));
            }
        }

        public void Remove(Course course)
        {
            Course removedCourse = Courses.FirstOrDefault(c => c.Id == course.Id);
            if (this is Student student)
            {
                foreach (var assignment in removedCourse.Assignments)
                {
                    student.Remove(assignment);
                }
                var grade = student.FinalGrades.FirstOrDefault(x => x.Key.Id == course.Id);
                student.FinalGrades.Remove(grade);
            }
            Courses.Remove(removedCourse);
        }
    }
}