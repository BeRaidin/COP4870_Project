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
        private ModuleService moduleService;
   
        public Module SelectedModule { get; set; }
        public string Code
        {
            get { return courseService.CurrentCourse.Code; }
        }

        public int Hours
        {
            get { return courseService.CurrentCourse.CreditHours; }
        }

        public string Name
        {
            get { return courseService.CurrentCourse.Name; }
        }

        public List<Person> Roster
        {
            get { return courseService.CurrentCourse.Roster; }
        }

        public List<Module> Modules
        {
            get { return courseService.CurrentCourse.Modules; }
        }
        public DetailedCourseViewModel()
        {
            courseService = CourseService.Current;
            personService = PersonService.Current;
            moduleService = ModuleService.Current;
        }

        public async void Add_Module()
        {
            var dialog = new AddModuleDialog();
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
        }

        public void UpdateCurrentModule()
        {
            moduleService.CurrentModule = SelectedModule;
        }
    }
}
