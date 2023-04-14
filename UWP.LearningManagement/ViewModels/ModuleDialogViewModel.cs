using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System.Collections.Generic;
using Windows.Networking;

namespace UWP.LearningManagement.ViewModels
{
    public class ModuleDialogViewModel
    {
        private readonly CourseService courseService;
        private readonly ModuleService moduleService;

        public Module SelectedModule
        {
            get { return moduleService.CurrentModule; }
            set { moduleService.CurrentModule = value; }
        }
        public Module Module { get; set; }
        public List<Module> Modules
        {
            get { return courseService.CurrentCourse.Modules; }
        }
        public string Name
        {
            get { return SelectedModule.Name; }
            set { SelectedModule.Name = value; }
        }
        public string Description
        {
            get { return SelectedModule.Description; }
            set { SelectedModule.Description = value; }
        }
        public string TempName { get; set; }
        public string TempDescription { get; set; }
        public bool IsCont;

        public ModuleDialogViewModel()
        {
            courseService = CourseService.Current;
            moduleService = ModuleService.Current;
            Module = new Module();
            IsCont = true;
        }

        public void Add()
        {
            bool test = true;
            foreach(var module in Modules)
            {
                if (module.Name == Name)
                {
                    test = false;
                }
            }
            if (test && Name != null && Name != "")
            {
                SelectedModule = new Module { Name = Name, Description = Description };
                Modules.Add(SelectedModule);
            }
        }

        public void Edit()
        {
            if (Name == null || Name == "" || Description == null || Description == "")
            {
                GetTemp();
            }
        }

        public void False()
        {
            IsCont = false;
        }

        public void SetTemp()
        {
            TempName = Name.ToString();
            TempDescription = Description.ToString();

        }

        public void GetTemp()
        {
            Name = TempName;
            Description = TempDescription;
        }
    }
}
