using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Collections;

namespace UWP.LearningManagement.ViewModels
{
    public class AnnouncementViewModel
    {
        private readonly CourseService courseService;
        public Course SelectedCourse
        {
            get { return courseService.CurrentCourse; }
            set { courseService.CurrentCourse = value; }
        }
        public Announcement Announcement { get; set; }
        public string Title 
        {
            get { return Announcement.Title; }
            set { Announcement.Title = value; } 
        }
        public string Message
        {
            get { return Announcement.Message; }
            set { Announcement.Message = value; }
        }

        public AnnouncementViewModel()
        {
            courseService = CourseService.Current;
            Announcement = new Announcement();
        }

        public void Add()
        {
            SelectedCourse.Add(Announcement);
        }
    }
}
