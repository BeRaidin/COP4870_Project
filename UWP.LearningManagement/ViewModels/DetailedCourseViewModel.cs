using Library.LearningManagement.Services;
using Library.LearningManagement.Models;
using UWP.LearningManagement.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.UI.Xaml.Controls;
using Windows.Foundation.Collections;
using System.Reflection.Metadata;

namespace UWP.LearningManagement.ViewModels
{
    public class DetailedCourseViewModel
    {
        private CourseService courseService;
        private PersonService personService;

        public string Code
        {
            get { return courseService.CurrentCourse.Code; }
        }

        public string Name
        {
            get { return courseService.CurrentCourse.Name; }
        }

        public List<Person> Roster
        {
            get { return courseService.CurrentCourse.Roster; }
        }

        public DetailedCourseViewModel()
        {
            courseService = CourseService.Current;
            personService = PersonService.Current;
        }
    }
}
