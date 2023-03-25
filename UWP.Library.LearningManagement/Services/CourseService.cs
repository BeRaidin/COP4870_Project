using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.LearningManagement.Models;

namespace Library.LearningManagement.Services
{
    public class CourseService
    {
        private IList<Course> courseList;
        private static CourseService instance;

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

        public void Add(Course course)
        {
            courseList.Add(course);
        }

        public IList<Course> Courses
        {
            get
            {
                return courseList;
            }
        }

        


        public IEnumerable<Course> Search(string query)
        {
            return Courses.Where(s => s.Code.ToUpper().Contains(query.ToUpper())
                || s.Name.ToUpper().Contains(query.ToUpper())
                || s.Description.ToUpper().Contains(query.ToUpper()));
        }
    }
}
