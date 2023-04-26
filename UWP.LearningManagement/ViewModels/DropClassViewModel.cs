using UWP.Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using UWP.LearningManagement.API.Util;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;

namespace UWP.LearningManagement.ViewModels
{
    public class DropClassViewModel
    {
        private List<Student> StudentList
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Person/GetStudents").Result;
                return JsonConvert.DeserializeObject<List<Student>>(payload);
            }
        }
        private IEnumerable<Instructor> InstructorList
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Person/GetInstructors").Result;
                return JsonConvert.DeserializeObject<List<Instructor>>(payload);
            }
        }
        private IEnumerable<TeachingAssistant> AssistantList
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Person/GetAssistants").Result;
                return JsonConvert.DeserializeObject<List<TeachingAssistant>>(payload);
            }
        }
        private IEnumerable<CourseViewModel> CourseList
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Course").Result;
                return JsonConvert.DeserializeObject<List<Course>>(payload).Select(d => new CourseViewModel(-1, d.Id));
            }
        }

        public Person Person { get; set; }
        public ObservableCollection<Course> Courses { get; set; }

        public DropClassViewModel(int id)
        {
            Person = StudentList.FirstOrDefault(x => x.Id == id);
            if (Person == null)
            {
                Person = InstructorList.FirstOrDefault(x => x.Id == id);
                if (Person == null)
                {
                    Person = AssistantList.FirstOrDefault(x => x.Id == id);
                }
            }

            Courses = new ObservableCollection<Course>();
            foreach (var course in CourseList)
            {
                if (course.Course.Roster.Any(x => x.Id == Person.Id))
                {
                    course.Course.IsSelected = false;
                    Courses.Add(course.Course);
                }
            }
        }

        public async Task Drop()
        {
            foreach (var course in Courses)
            {
                if (course.IsSelected)
                {
                    course.Remove(Person);
                    var payload = await new WebRequestHandler().Post("http://localhost:5159/Course/UpdateRoster", course);
                    var returnVal = JsonConvert.DeserializeObject<Course>(payload);
                    Person.Remove(returnVal);
                    if (Person is Student student)
                    {
                        await new WebRequestHandler().Post("http://localhost:5159/Person/UpdateStudentCourses", student);
                    }
                    else
                    {
                        await new WebRequestHandler().Post("http://localhost:5159/Person/UpdateCourses", Person);
                    }
                }
            }
        }
    }
}