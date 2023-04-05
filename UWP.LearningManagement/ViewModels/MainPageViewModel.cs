using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagement.ViewModels
{
    public class MainPageViewModel
    {
        

        private readonly PersonService personService;
        private readonly ModuleService moduleService;
        private readonly CourseService courseService;

        public string Semester { get; set; }
        public int Year { get; set; }

        public MainPageViewModel() 
        { 
            personService = PersonService.Current;
            moduleService = ModuleService.Current;
            courseService = CourseService.Current;
            Semester = "Spring";
            Year = 2023;
        }

        public void Clear()
        {
            personService.CurrentPerson = null;
            personService.CurrentAssignment = null;
            moduleService.CurrentModule = null;
            moduleService.CurrentItem = null;
            courseService.CurrentCourse = null;
        }

        public void LeftClick()
        {
            if (Semester.Equals("Fall"))
            {
                Semester = "Spring";
                Year++;
            }
            else if (Semester.Equals("Summer"))
            {
                Semester = "Fall";
            }
            else if (Semester.Equals("Spring"))
            {
                Semester = "Summer";
            }
        }

        public void RightClick()
        {
            if (Semester.Equals("Spring"))
            {
                Semester = "Fall";
                Year--;
            }
            else if (Semester.Equals("Summer"))
            {
                Semester = "Spring";
            }
            else if (Semester.Equals("Fall"))
            {
                Semester = "Summer";
            }
        }
    }
}
