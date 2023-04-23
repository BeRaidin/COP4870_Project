using UWP.Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System.Collections.ObjectModel;
using UWP.LearningManagement.API.Util;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace UWP.LearningManagement.ViewModels
{
    public class InstructorJoinCourseDialogViewModel
    {
        private readonly CourseService courseService;
        private readonly PersonService personService;
        private readonly SemesterService semesterService;

        public IEnumerable<CourseViewModel> AllCourses
        {
            get 
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Course").Result;
                var returnVal = JsonConvert.DeserializeObject<List<Course>>(payload).Select(d => new CourseViewModel(new InstructorDetailsViewModel(), d));
                return returnVal;
            }
        }

        public Course SelectedCourse
        {
            get { return courseService.CurrentCourse; }
            set { courseService.CurrentCourse = value; }
        }
        public Person SelectedPerson
        { 
            get { return personService.CurrentPerson; } 
        }
        public Semester SelectedSemester
        {
            get { return  semesterService.CurrentSemester; }
        }

        public ObservableCollection<Course> AvailableCourses { get; set; }

        public InstructorJoinCourseDialogViewModel() 
        {
            courseService = CourseService.Current;
            personService = PersonService.Current;
            semesterService = SemesterService.Current;
            AvailableCourses = new ObservableCollection<Course>();
            foreach(var courseModel in AllCourses)
            {
                if(!SelectedPerson.Courses.Contains(courseModel.Course))
                {
                    AvailableCourses.Add(courseModel.Course);
                }
            }
        }

        public void Join()
        {
            if(SelectedCourse != null) 
            {
                SelectedPerson.Add(SelectedCourse);
                SelectedCourse.Add(SelectedPerson);
            }
        }
    }
}
