using UWP.Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System.Collections.ObjectModel;
using UWP.LearningManagement.API.Util;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Windows.ApplicationModel.Activation;

namespace UWP.LearningManagement.ViewModels
{
    public class InstructorJoinCourseDialogViewModel
    {
        private readonly CourseService courseService;
        private readonly SemesterService semesterService;
        private readonly int Id;

        public IEnumerable<Course> AllCourses
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Course").Result;
                var returnVal = JsonConvert.DeserializeObject<List<Course>>(payload);
                return returnVal;
            }
        }
        public IEnumerable<Person> AllPeople
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Person/GetPeople").Result;
                var returnVal = JsonConvert.DeserializeObject<List<Person>>(payload);
                return returnVal;
            }
        }


        public Course SelectedCourse { get; set; }
        public Person Person { get; set; }
        public Semester SelectedSemester
        {
            get { return semesterService.CurrentSemester; }
        }

        public ObservableCollection<Course> AvailableCourses { get; set; }

        public InstructorJoinCourseDialogViewModel(int id)
        {
            Id = id;
            courseService = CourseService.Current;
            semesterService = SemesterService.Current;
            Person = AllPeople.FirstOrDefault(p => p.Id == Id);
            AvailableCourses = new ObservableCollection<Course>();
            foreach (var course in AllCourses)
            {
                bool hasSameId = Person.Courses.Any(c => c.Id == course.Id);
                if(!hasSameId)
                {
                    AvailableCourses.Add(course);
                }

            }
        }

        public async void Join()
        {
            if (SelectedCourse != null)
            {
                SelectedCourse.Add(Person);
                var returnVal = await new WebRequestHandler().Post("http://localhost:5159/Course/UpdateRoster", SelectedCourse);
                Course deserializedReturn = JsonConvert.DeserializeObject<Course>(returnVal);

                Person.Add(deserializedReturn);
                await new WebRequestHandler().Post("http://localhost:5159/Person/UpdateCourses", Person);

                Person = AllPeople.FirstOrDefault(p => p.Id == Id);
            }
        }
    }
}
