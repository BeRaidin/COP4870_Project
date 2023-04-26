using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWP.LearningManagement.API.Util;
using UWP.Library.LearningManagement.Models;

namespace UWP.LearningManagement.ViewModels
{
    public class EditCoursesViewModel
    {
        private IEnumerable<CourseViewModel> CourseList
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Course").Result;
                var returnVal = JsonConvert.DeserializeObject<List<Course>>(payload).Select(d => new CourseViewModel(-1, d.Id));
                return returnVal;
            }
        }
        private IEnumerable<Student> StudentList
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Person/GetStudents").Result;
                var returnVal = JsonConvert.DeserializeObject<List<Student>>(payload);
                return returnVal;
            }
        }
        private IEnumerable<Instructor> InstructorList
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Person/GetInstructors").Result;
                var returnVal = JsonConvert.DeserializeObject<List<Instructor>>(payload);
                return returnVal;
            }
        }
        private IEnumerable<TeachingAssistant> AssistantList
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Person/GetAssistants").Result;
                var returnVal = JsonConvert.DeserializeObject<List<TeachingAssistant>>(payload);
                return returnVal;
            }
        }

        public ObservableCollection<CourseViewModel> Courses { get; set; }
        public CourseViewModel SelectedCourse { get; set; }

        public string Query { get; set; }

        public EditCoursesViewModel()
        {
            Courses = new ObservableCollection<CourseViewModel>(CourseList);
        }

        public void Search()
        {
            if (Query != null && Query != "")
            {
                IEnumerable<CourseViewModel> searchResults;
                if (int.TryParse(Query, out int id))
                {
                    searchResults = CourseList.Where(i => i.Course.Id == id).ToList();
                }
                else
                {
                    searchResults = CourseList.Where(i => i.Course.Code.Contains(Query, StringComparison.InvariantCultureIgnoreCase));
                }

                Courses.Clear();
                foreach (var course in searchResults)
                {
                    Courses.Add(course);
                }
            }
            else
            {
                Courses.Clear();
                foreach (var course in CourseList)
                {
                    Courses.Add(course);
                }
            }
        }

        public async Task Delete()
        {
            if (SelectedCourse != null)
            {
                foreach (var person in SelectedCourse.Course.Roster)
                {
                    Person editedPerson = StudentList.FirstOrDefault(x => x.Id == person.Id);
                    if (editedPerson == null)
                    {
                        editedPerson = InstructorList.FirstOrDefault(x => x.Id == person.Id);
                        if (editedPerson == null)
                        {
                            editedPerson = AssistantList.FirstOrDefault(x => x.Id == person.Id);
                        }
                    }
                    editedPerson.Remove(SelectedCourse.Course);
                    if(editedPerson is Student student)
                    {
                        await new WebRequestHandler().Post("http://localhost:5159/Person/UpdateStudentCourses", student);
                    }
                    else
                    {
                        await new WebRequestHandler().Post("http://localhost:5159/Person/UpdateCourses", editedPerson);
                    }
                }
                await new WebRequestHandler().Post("http://localhost:5159/Course/Delete", SelectedCourse.Course);
            }
        }
    }
}
