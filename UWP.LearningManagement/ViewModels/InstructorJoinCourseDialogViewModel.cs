using UWP.Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System.Collections.ObjectModel;

namespace UWP.LearningManagement.ViewModels
{
    public class InstructorJoinCourseDialogViewModel
    {
        private readonly CourseService courseService;
        private readonly PersonService personService;
        private readonly SemesterService semesterService;

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
            foreach(var course in SelectedSemester.Courses)
            {
                if(!SelectedPerson.Courses.Contains(course))
                {
                    AvailableCourses.Add(course);
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
