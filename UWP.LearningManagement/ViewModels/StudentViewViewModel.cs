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
        private readonly SemesterService semesterService;

        private IEnumerable<Student> AllStudents
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Student").Result;
                var returnVal = JsonConvert.DeserializeObject<List<Student>>(payload);
                return returnVal;
            }
        }
        
        public ObservableCollection<Student> Students { get; set; }
        public Student SelectedStudent { get; set; }
        public Semester SelectedSemester
        {
            get { return semesterService.CurrentSemester; }
        }
        public string Query { get; set; }
        public string Period
        { 
            get { return SelectedSemester.Period; } 
        }
        public int Year
        {
            get { return SelectedSemester.Year; }
        }

        public StudentViewViewModel() 
        {
            semesterService = SemesterService.Current;
            Students = new ObservableCollection<Student>(AllStudents);
        }

        public void Search()
        {
            if (Query != null && Query != "")
            {
                IEnumerable<Student> searchResults;
                if (int.TryParse(Query, out int id))
                {
                    searchResults = AllStudents.Where(i => i.Id == id).ToList();
                }
                else
                {
                    searchResults = AllStudents.Where(i => i.FirstName.Contains(Query, StringComparison.InvariantCultureIgnoreCase));
                }


                Students.Clear();
                foreach (var person in searchResults)
                {
                    Students.Add(person);
                }
            }
            else
            {
                Refresh();
            }
        }

        public void Refresh()
        {
            Students.Clear();
            foreach (var person in AllStudents)
            {
                Students.Add(person);
            }
        }
    }
}
