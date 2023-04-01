using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Collections;
using Windows.UI.Xaml.Automation;

namespace UWP.LearningManagement.ViewModels
{
    internal class AddEditPageViewModel
    {
        private readonly PersonService personService;
        private readonly CourseService courseService;
        private readonly List<Course> courses;
        private readonly List<Person> allPeople;

        public Person SelectedPerson
        {
            get { return personService.CurrentPerson; }
            set { personService.CurrentPerson = value; }
        }
        public Course SelectedCourse
        {
            get { return courseService.CurrentCourse; }
            set { courseService.CurrentCourse = value; }
        }

        public AddEditPageViewModel()
        {
            personService = PersonService.Current;
            courseService = CourseService.Current;
        }

        public void Search()
        {

        }

        public void UpdatePerson()
        {

        }

        public void UpdateCourse()
        {

        }

        public void Add()
        {

        }

        public void Delete()
        {

        }
    }
}
