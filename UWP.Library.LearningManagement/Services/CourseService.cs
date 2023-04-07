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
        public List<Course> CourseList
        {
            get
            {
                return courseList;
            }
            set
            {
                courseList = value;
            }
        }
        private Course _currentCourse;
        public Course CurrentCourse
        {
            get { return _currentCourse; }
            set { _currentCourse = value; }
        }
        public Announcement CurrentAnnouncement { get; set; }
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
        private static CourseService instance;

        public CourseService()
        {
            CourseList = new List<Course>();   
        }        

        public void Add(Course course)
        {
            CourseList.Add(course);
        }

        public void Remove()
        {
            foreach (var course in CourseList)
            {
                if(course.Code == CurrentCourse.Code)
                {
                    CourseList.Remove(course);
                    break;
                }
            }
        }
    }
}
