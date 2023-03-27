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
            courseList = new List<Course> {
                new Course{Code = "test1", Name= "name1"},
                new Course{Code = "test2", Name= "name2"} 
            };
                
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
            courseList.Remove(currentCourse);
        }

        public IEnumerable<Course> Search(string query)
        {
            return courseList.Where(s => s.Code.ToUpper().Contains(query.ToUpper())
                || s.Name.ToUpper().Contains(query.ToUpper())
                || s.Description.ToUpper().Contains(query.ToUpper()));
        }
    }
}
