﻿using UWP.Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using UWP.LearningManagement.API.Util;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;

namespace UWP.LearningManagement.ViewModels
{
    internal class RosterViewModel
    {
        private readonly CourseService courseService;
        private readonly SemesterService semesterService;
        private IEnumerable<StudentViewModel> AllStudents
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Person/GetStudents").Result;
                var returnVal = JsonConvert.DeserializeObject<List<Student>>(payload).Select(d => new StudentViewModel(d.Id));
                return returnVal;
            }
        }
        public ObservableCollection<Student> Students { get; set; }
        public Semester SelectedSemester { get { return semesterService.CurrentSemester; } }
        public Course Course 
        {
            get { return courseService.CurrentCourse; }
        }

        public RosterViewModel()
        {
            courseService = CourseService.Current;
            semesterService = SemesterService.Current;

            Students = new ObservableCollection<Student>();
            foreach(var student in  AllStudents)
            {
                Students.Add(student.Student);
            }

            foreach (var student in Students)
            {
                if(Course.Roster.Contains(student))
                {
                    student.IsSelected = true;
                }
                else
                {
                    student.IsSelected = false;
                }
            }
        }

        public async Task AddRoster()
        {
            foreach(var student in Students)
            {
                if (student.IsSelected == false && Course.Roster.Contains(student))
                {
                    Course.Remove(student);
                    var payload = await new WebRequestHandler().Post("http://localhost:5159/Course/UpdateRoster", Course);
                    var returnVal = JsonConvert.DeserializeObject<Course>(payload);
                    student.Remove(returnVal);
                    await new WebRequestHandler().Post("http://localhost:5159/Person/UpdateStudentCourses", student);
                }
                else if (student.IsSelected == true && !Course.Roster.Contains(student)) 
                { 
                    Course.Add(student);
                    var payload = await new WebRequestHandler().Post("http://localhost:5159/Course/UpdateRoster", Course);
                    var returnVal = JsonConvert.DeserializeObject<Course>(payload);
                    student.Add(returnVal);
                    await new WebRequestHandler().Post("http://localhost:5159/Person/UpdateStudentCourses", student);
                }
            }
        }
    }
}
