using UWP.Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        public bool IsValid;

        public AnnouncementViewModel()
        {
            courseService = CourseService.Current;
            Announcement = new Announcement();
            IsValid = true;
        }

        public void Add()
        {
            bool isNew = true;
            foreach(var announcement in SelectedCourse.Announcements)
            {
                if (announcement.Title == Title)
                {
                    isNew = false;
                    break;
                }
            }

            if (Title != null && Title != "" && Message != null && Message != "" && isNew)
            {
                SelectedCourse.Add(Announcement);
            }
            else IsValid = false;
        }

        public void Edit()
        {
            foreach (var announcement in SelectedCourse.Announcements)
            {
                if (Title == announcement.Title)
                {
                    IsValid = false;
                }
            }
            if (Message == null || Message == "")
            {
                IsValid = false;
            }
            if (IsValid)
            {
                courseService.CurrentAnnouncement.Title = Title;
                courseService.CurrentAnnouncement.Message = Message;
            }
        }
    }
}
