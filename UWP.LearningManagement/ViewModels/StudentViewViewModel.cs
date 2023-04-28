using Library.LearningManagement.Services;
using UWP.Library.LearningManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UWP.LearningManagement.API.Util;
using Newtonsoft.Json;

namespace UWP.LearningManagement.ViewModels
{
    public class StudentViewViewModel
    {
        private IEnumerable<StudentViewModel> StudentList
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Person/GetStudents").Result;
                return JsonConvert.DeserializeObject<List<Student>>(payload).Select(d => new StudentViewModel(d.Id));
            }
        }
        private ListNavigator<StudentViewModel> StudentListNav { get; set; }
        public ObservableCollection<StudentViewModel> Students { get; set; }
        public StudentViewModel SelectedStudent { get; set; }
        public Semester Semester { get; }
        public string Query { get; set; }


        public StudentViewViewModel()
        {
            StudentListNav = new ListNavigator<StudentViewModel>(StudentList.ToList());
            Students = new ObservableCollection<StudentViewModel>();
            Refresh();
            var payload = new WebRequestHandler().Get("http://localhost:5159/Semester/GetCurrentSemester").Result;
            Semester = JsonConvert.DeserializeObject<List<Semester>>(payload)[0];
        }

        public void Search()
        {
            if (Query != null && Query != "")
            {
                IEnumerable<StudentViewModel> searchResults;
                if (int.TryParse(Query, out int id))
                {
                    searchResults = StudentList.Where(i => i.Student.Id == id).ToList();
                }
                else
                {
                    searchResults = StudentList.Where(i => i.Student.FirstName.Contains(Query, StringComparison.InvariantCultureIgnoreCase));
                }

                StudentListNav = new ListNavigator<StudentViewModel>(searchResults.ToList());
                Students.Clear();
                if (StudentListNav.State.Count > 0)
                {
                    foreach (var item in StudentListNav.GetCurrentPage())
                    {
                        Students.Add(item.Value);
                    }
                }
            }
            else
            {
                Refresh();
            }
        }

        public void Refresh()
        {
            StudentListNav = new ListNavigator<StudentViewModel>(StudentList.ToList());
            Students.Clear();
            if (StudentListNav.State.Count > 0)
            {
                foreach (var item in StudentListNav.GetCurrentPage())
                {
                    Students.Add(item.Value);
                }
            }
        }

        public void NextPage()
        {
            if (StudentListNav.HasNextPage)
            {
                StudentListNav.GoForward();
                Students.Clear();
                if (StudentListNav.State.Count > 0)
                {
                    foreach (var item in StudentListNav.GetCurrentPage())
                    {
                        Students.Add(item.Value);
                    }
                }
            }
        }

        public void PreviousPage()
        {
            if (StudentListNav.HasPreviousPage)
            {
                StudentListNav.GoBackward();
                Students.Clear();
                if (StudentListNav.State.Count > 0)
                {
                    foreach (var item in StudentListNav.GetCurrentPage())
                    {
                        Students.Add(item.Value);
                    }
                }
            }
        }
    }
}
