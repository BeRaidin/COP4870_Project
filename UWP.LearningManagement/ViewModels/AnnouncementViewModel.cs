using UWP.Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Collections;
using Newtonsoft.Json;
using UWP.LearningManagement.API.Util;

namespace UWP.LearningManagement.ViewModels
{
    public class AnnouncementViewModel
    {
        public IEnumerable<Announcement> Announcements
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Announcement").Result;
                return JsonConvert.DeserializeObject<List<Announcement>>(payload);
            }
        }
        public IEnumerable<Course> Courses
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Course").Result;
                return JsonConvert.DeserializeObject<List<Course>>(payload);
            }
        }

        public virtual string Display => $"{Title}";


        public Course Course { get; set; }
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
            Announcement = new Announcement();
            IsValid = true;
        }

        public AnnouncementViewModel(int id, int courseId = -1)
        {
            if (id != -1)
            {
                Announcement = Announcements.FirstOrDefault(x => x.Id == id);
            }
            else Announcement = new Announcement { Id = -1 };
            if (courseId != -1)
            {
                Course = Courses.FirstOrDefault(x => x.Id == courseId);
            }
        }

        public async Task<Announcement> Add()
        {
            var handler = new WebRequestHandler();
            var returnVal = await handler.Post("http://localhost:5159/Announcement/AddOrUpdate", Announcement);
            var deserializedReturn = JsonConvert.DeserializeObject<Announcement>(returnVal);
            Course.Add(deserializedReturn);
            await new WebRequestHandler().Post("http://localhost:5159/Course/UpdateAnnouncements", Course);
            return deserializedReturn;
        }

        public void Edit()
        {
            foreach (var announcement in Course.Announcements)
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
            //if (IsValid)
            //{
            //    courseService.CurrentAnnouncement.Title = Title;
            //    courseService.CurrentAnnouncement.Message = Message;
            //}
        }
    }
}
