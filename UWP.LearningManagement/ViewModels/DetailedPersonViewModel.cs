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
    internal class DetailedPersonViewModel
    {
        private CourseService courseService;
        private PersonService personService;

        public string Name
        {
            get { return personService.CurrentPerson.Name; }
        }

        public List<Course> Courses 
        {
            get { return personService.CurrentPerson.Courses; }
        }

        public DetailedPersonViewModel()
        {
            courseService = CourseService.Current;
            personService = PersonService.Current;
        }


    }
}
