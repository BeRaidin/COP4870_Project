using UWP.Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System.Collections.Generic;
using Windows.Networking;
using Newtonsoft.Json;
using UWP.LearningManagement.API.Util;
using System.Linq;
using System.Threading.Tasks;

namespace UWP.LearningManagement.ViewModels
{
    public class ModuleViewModel
    {
        private readonly CourseService courseService;
        private readonly ModuleService moduleService;
        public IEnumerable<Module> Modules
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Module").Result;
                return JsonConvert.DeserializeObject<List<Module>>(payload);
            }
        }
        public IEnumerable<Course> Courses
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Course").Result;
                return JsonConvert.DeserializeObject<List<Course>>(payload);
            }
        }

        public Module SelectedModule
        {
            get { return moduleService.CurrentModule; }
            set { moduleService.CurrentModule = value; }
        }
        public Module Module { get; set; }
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
        public bool IsValid;


        public Course Course { get; set; }

        public ModuleViewModel()
        {
            courseService = CourseService.Current;
            moduleService = ModuleService.Current;
            Module = new Module();
            IsCont = true;
            IsValid = true;
        }
        public ModuleViewModel(int id, int courseId = -1)
        {
            courseService = CourseService.Current;
            moduleService = ModuleService.Current;

            if (id != -1)
            {
                Module = Modules.FirstOrDefault(x => x.Id == id);
            }
            else Module = new Module { Id = -1 };
            if (courseId != -1)
            {
                Course = Courses.FirstOrDefault(x => x.Id == courseId);
            }
            IsCont = true;
            IsValid = true;
        }

        public virtual string Display => $"{Module.Name} - {Module.Description}";

        public async Task<Module> Add()
        {
            var handler = new WebRequestHandler();
            var returnVal = await handler.Post("http://localhost:5159/Module/AddOrUpdate", Module);
            var deserializedReturn = JsonConvert.DeserializeObject<Module>(returnVal);
            Course.Add(deserializedReturn);
            await new WebRequestHandler().Post("http://localhost:5159/Course/UpdateModules", Course);
            return deserializedReturn;
        }

        public void Edit()
        {
            if (Name == null || Name == "" || Description == null || Description == "")
            {
                GetTemp();
                IsValid = false;
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
