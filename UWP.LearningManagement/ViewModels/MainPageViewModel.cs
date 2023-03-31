using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagement.ViewModels
{
    public class MainPageViewModel
    {
        private readonly PersonService personService;
        private readonly ModuleService moduleService;
        private readonly CourseService courseService;

        public MainPageViewModel() 
        { 
            personService = PersonService.Current;
            moduleService = ModuleService.Current;
            courseService = CourseService.Current;
        }

        public void Clear()
        {
            personService.CurrentPerson = null;
            personService.CurrentAssignment = null;
            moduleService.CurrentModule = null;
            moduleService.CurrentItem = null;
            courseService.CurrentCourse = null;
        }
    }
}
