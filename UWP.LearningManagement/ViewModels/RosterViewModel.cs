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
        private readonly SemesterService semesterService;
        private IEnumerable<StudentViewModel> StudentList
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Person/GetStudents").Result;
                var returnVal = JsonConvert.DeserializeObject<List<Student>>(payload).Select(d => new StudentViewModel(d.Id));
                return returnVal;
            }
        }
        private IEnumerable<Course> CourseList
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Course").Result;
                var returnVal = JsonConvert.DeserializeObject<List<Course>>(payload);
                return returnVal;
            }
        }
        private IEnumerable<Assignment> AssignmentList
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Assignment").Result;
                var returnVal = JsonConvert.DeserializeObject<List<Assignment>>(payload);
                return returnVal;
            }
        }
        public ObservableCollection<Student> Students { get; set; }
        public Semester SelectedSemester { get { return semesterService.CurrentSemester; } }
        public Course Course { get; set; }

        public RosterViewModel(int id)
        {
            Course = CourseList.FirstOrDefault(c => c.Id == id);
            semesterService = SemesterService.Current;

            Students = new ObservableCollection<Student>();
            foreach (var student in StudentList)
            {
                Students.Add(student.Student);
            }

            foreach (var student in Students)
            {
                if (Course.Roster.Any(x => x.Id == student.Id))
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
            foreach (var student in Students)
            {
                Student editedStudent = StudentList.FirstOrDefault(x => x.Student.Id == student.Id).Student;

                if (student.IsSelected == false && Course.Roster.Any(x => x.Id == student.Id))
                {
                    Course.Remove(editedStudent);
                    var payload = await new WebRequestHandler().Post("http://localhost:5159/Course/UpdateRoster", Course);
                    var returnVal = JsonConvert.DeserializeObject<Course>(payload);
                    editedStudent.Remove(returnVal);
                    await new WebRequestHandler().Post("http://localhost:5159/Person/UpdateStudentCourses", editedStudent);
                }
                else if (student.IsSelected == true && !Course.Roster.Any(x => x.Id == student.Id))
                {
                    editedStudent.Add(Course);
                    await new WebRequestHandler().Post("http://localhost:5159/Person/UpdateStudentCourses", editedStudent);

                    var editedCourse = CourseList.FirstOrDefault(x => x.Id == Course.Id);
                    editedCourse.Add(student);
                    var payload = await new WebRequestHandler().Post("http://localhost:5159/Course/UpdateRoster", editedCourse);
                    var returnVal = JsonConvert.DeserializeObject<Course>(payload);
                }
            }
        }
    }
}
