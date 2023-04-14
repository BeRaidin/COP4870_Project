using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System.Collections.ObjectModel;

namespace UWP.LearningManagement.ViewModels
{
    public class InstructorJoinCourseDialogViewModel
    {
        private readonly CourseService courseService;
        private readonly PersonService personService;

        public Course SelectedCourse
        {
            get { return courseService.CurrentCourse; }
            set { courseService.CurrentCourse = value; }
        }
        public Person SelectedPerson
        { 
            get { return personService.CurrentPerson; } 
        }

        public ObservableCollection<Course> AvailableCourses { get; set; }

        public InstructorJoinCourseDialogViewModel() 
        {
            courseService = CourseService.Current;
            personService = PersonService.Current;
            AvailableCourses = new ObservableCollection<Course>();
            foreach(var course in courseService.Courses)
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
