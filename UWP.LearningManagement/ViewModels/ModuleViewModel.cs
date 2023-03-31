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
        private readonly CourseService courseService;

        private Module _module;
        public Module Module
        {
            get { return _module; }
            set { _module = value; }
        }

        public List<Module> Modules
        {
            get { return courseService.CurrentCourse.Modules; }
        }

        public string Name
        {
            get { return Module.Name; }
            set { Module.Name = value; }
        }
        public string Description
        {
            get { return Module.Description; }
            set { Module.Description = value; }
        }

        public ModuleViewModel()
        {
            courseService = CourseService.Current;
            Module = new Module();
        }

        public void Add()
        {
            Modules.Add(Module);
        }
    }
}
