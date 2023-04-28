using UWP.Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UWP.LearningManagement.Dialogs;
using UWP.Library.LearningManagement.Database;
using Newtonsoft.Json;
using UWP.LearningManagement.API.Util;
using Windows.Devices.Bluetooth.Advertisement;
using System.Threading.Tasks;

namespace UWP.LearningManagement.ViewModels
{
    public class InstructorViewViewModel
    {
        private IEnumerable<AdminViewModel> InstructorList
        {
            get
            {
                var payloadInstructors = new WebRequestHandler().Get("http://localhost:5159/Person/GetInstructors").Result;
                var payloadAssistants = new WebRequestHandler().Get("http://localhost:5159/Person/GetAssistants").Result;
                var instructorsList = JsonConvert.DeserializeObject<List<Instructor>>(payloadInstructors).Select(d => new AdminViewModel(d.Id));
                var assistantsList = JsonConvert.DeserializeObject<List<TeachingAssistant>>(payloadAssistants).Select(d => new AdminViewModel(d.Id));
                List<AdminViewModel> results = new List<AdminViewModel>();
                foreach (var instructor in instructorsList)
                {
                    results.Add(instructor);
                }
                foreach (var teachingAssistant in assistantsList)
                {
                    results.Add(teachingAssistant);
                }

                IEnumerable<AdminViewModel> returnVal = results;

                return returnVal;
            }
        }
        private IEnumerable<Course> CourseList
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Course").Result;
                return JsonConvert.DeserializeObject<List<Course>>(payload);
            }
        }
        private ListNavigator<AdminViewModel> InstructorListNav { get; set; }

        public ObservableCollection<AdminViewModel> Instructors { get; set; }
        public AdminViewModel SelectedInstructor { get; set; }

        public Semester Semester { get; }
        public string Query { get; set; }

        public InstructorViewViewModel()
        {
            InstructorListNav = new ListNavigator<AdminViewModel>(InstructorList.ToList());
            Instructors = new ObservableCollection<AdminViewModel>();
            Refresh();
            var payload = new WebRequestHandler().Get("http://localhost:5159/Semester/GetCurrentSemester").Result;
            Semester = JsonConvert.DeserializeObject<List<Semester>>(payload)[0];
        }

        public void Search()
        {
            if (Query != null && Query != "")
            {
                IEnumerable<AdminViewModel> searchResults;
                if (int.TryParse(Query, out int id))
                {
                    searchResults = InstructorList.Where(i => i.Person.Id == id).ToList();
                }
                else
                {
                    searchResults = InstructorList.Where(i => i.Person.FirstName.Contains(Query, StringComparison.InvariantCultureIgnoreCase));
                }
                InstructorListNav = new ListNavigator<AdminViewModel>(searchResults.ToList());
                Instructors.Clear();
                if (InstructorListNav.State.Count > 0)
                {
                    foreach (var item in InstructorListNav.GetCurrentPage())
                    {
                        Instructors.Add(item.Value);
                    }
                }
            }
            else
            {
                Refresh();
            }
        }

        public async Task Delete()
        {
            if (SelectedInstructor != null)
            {
                foreach (var course in SelectedInstructor.Person.Courses)
                {
                    Course editedCourse = CourseList.FirstOrDefault(x => x.Id == course.Id);
                    editedCourse.Remove(SelectedInstructor.Person);
                    await new WebRequestHandler().Post("http://localhost:5159/Course/UpdateRoster", editedCourse);
                }
                await new WebRequestHandler().Post("http://localhost:5159/Person/Delete", SelectedInstructor.Person);
                Refresh();
            }
        }

        public void Refresh()
        {
            InstructorListNav = new ListNavigator<AdminViewModel>(InstructorList.ToList());
            Instructors.Clear();
            if (InstructorListNav.State.Count > 0)
            {
                foreach (var item in InstructorListNav.GetCurrentPage())
                {
                    Instructors.Add(item.Value);
                }
            }
        }

        public void NextPage()
        {
            if(InstructorListNav.HasNextPage)
            {
                InstructorListNav.GoForward();
                Instructors.Clear();
                if (InstructorListNav.State.Count > 0)
                {
                    foreach (var item in InstructorListNav.GetCurrentPage())
                    {
                        Instructors.Add(item.Value);
                    }
                }
            }
        }

        public void PreviousPage()
        {
            if (InstructorListNav.HasPreviousPage)
            {
                InstructorListNav.GoBackward();
                Instructors.Clear();
                if (InstructorListNav.State.Count > 0)
                {
                    foreach (var item in InstructorListNav.GetCurrentPage())
                    {
                        Instructors.Add(item.Value);
                    }
                }
            }
        }
    }
}
