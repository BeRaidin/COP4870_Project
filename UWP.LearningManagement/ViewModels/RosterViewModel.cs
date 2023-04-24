using UWP.Library.LearningManagement.Models;
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
        public IEnumerable<StudentViewModel> AllStudents
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Person/GetStudents").Result;
                var returnVal = JsonConvert.DeserializeObject<List<Student>>(payload).Select(d => new StudentViewModel(d)); ;
                return returnVal;
            }
        }
        public Semester SelectedSemester { get { return semesterService.CurrentSemester; } }
        public Course Course 
        {
            get { return courseService.CurrentCourse; }
        }

        public RosterViewModel()
        {
            courseService = CourseService.Current;
            semesterService = SemesterService.Current;
            foreach (var student in AllStudents)
            {
                if(Course.Roster.Contains(student.Student))
                {
                    student.Student.IsSelected = true;
                }
                else
                {
                    student.Student.IsSelected = false;
                }
            }
        }

        public async Task AddRoster()
        {
            foreach(var student in AllStudents)
            {
                if (student.Student.IsSelected == false && Course.Roster.Contains(student.Student))
                {
                    Course.Remove(student.Student);
                    await new WebRequestHandler().Post("http://localhost:5159/Course/UpdateRoster", Course);
                    var payload = await new WebRequestHandler().Post("http://localhost:5159/Person/UpdateCourses", student);

                }
                else if (student.Student.IsSelected == true && !Course.Roster.Contains(student.Student)) 
                { 
                    Course.Add(student.Student);
                    await new WebRequestHandler().Post("http://localhost:5159/Course/UpdateRoster", Course);
                    var payload = await new WebRequestHandler().Post("http://localhost:5159/Person/UpdateCourses", student);
                }
            }
        }
    }
}
