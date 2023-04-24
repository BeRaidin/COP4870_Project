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
        private readonly PersonService personService;
        private readonly SemesterService semesterService;

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


        public Course SelectedCourse
        {
            get { return courseService.CurrentCourse; }
            set { courseService.CurrentCourse = value; }
        }
        public Person SelectedPerson { get; set; }
        public Semester SelectedSemester
        {
            get { return  semesterService.CurrentSemester; }
        }

        public ObservableCollection<Course> AvailableCourses { get; set; }

        public InstructorJoinCourseDialogViewModel(int id) 
        {
            courseService = CourseService.Current;
            personService = PersonService.Current;
            semesterService = SemesterService.Current;
            SelectedPerson = AllPeople.FirstOrDefault(p => p.Id == id);
            AvailableCourses = new ObservableCollection<Course>();
            foreach(var course in AllCourses)
            {
                if(!SelectedPerson.Courses.Contains(course))
                {
                    AvailableCourses.Add(course);
                }
            }
        }

        public async void Join()
        {
            if(SelectedCourse != null) 
            {
                SelectedPerson.Add(SelectedCourse);
                var payload = await new WebRequestHandler().Post("http://localhost:5159/Person/UpdateCourses", SelectedPerson);
                var returnVal = JsonConvert.DeserializeObject<Person>(payload);

                SelectedCourse.Add(returnVal);
                await new WebRequestHandler().Post("http://localhost:5159/Course/UpdateRoster", SelectedCourse);
            }
        }
    }
}
