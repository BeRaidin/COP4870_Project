using Library.LearningManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Services
{
    public class CourseService
    {
        private List<Course> courseList;
        private static CourseService instance;
        private Course currentCourse;
        public Course CurrentCourse
        {
            get { return currentCourse; }
            set { currentCourse = value; }
        }

        public CourseService()
        {
            courseList = new List<Course>();
                
        }

        public static CourseService Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new CourseService();
                }

                return instance;
            }
        }

        public List<Course> Courses
        {
            get 
            { 
                return courseList; 
            }
        }

        public void Add(Course course)
        {
            courseList.Add(course);
        }

        public void Remove()
        {
            foreach( var person in currentCourse.Roster)
            {
                person.Courses.Remove(currentCourse);
            }
            courseList.Remove(currentCourse);
        }
    }
}
