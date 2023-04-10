using Library.LearningManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWP.Library.LearningManagement.Database;

namespace Library.LearningManagement.Services
{
    public class CourseService
    {
        public List<Course> Courses
        { get { return FakeDataBase.Courses; } }


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
        }        

        public void Add(Course course)
        {
            FakeDataBase.Courses.Add(course);
        }

        public void Remove()
        {
            foreach (var course in FakeDataBase.Courses)
            {
                if(course.Code == CurrentCourse.Code)
                {
                    FakeDataBase.Courses.Remove(course);
                    break;
                }
            }
        }
    }
}
