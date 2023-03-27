using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.UserActivities.Core;

namespace UWP.LearningManagement.ViewModels
{
    public class ModuleViewModel
    {
        private CourseService courseService;
        private PersonService personService;

        private Module module { get; set; }
        public Module Module
        {
            get { return module; }
            set { value = module; }
        }

        public List<Module> Modules
        {
            get { return courseService.CurrentCourse.Modules; }
        }

        public string Name
        {
            get { return module.Name; }
            set { module.Name = value; }
        }
        public string Description
        {
            get { return module.Description; }
            set { module.Description = value; }
        }

        public ModuleViewModel()
        {
            courseService = CourseService.Current;
            personService = PersonService.Current;
            module = new Module();
        }

        public void Add()
        {
            courseService.CurrentCourse.Modules.Add(module);
        }
    }
}
