using UWP.Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System.Collections.Generic;
using Windows.Networking;
using Newtonsoft.Json;
using UWP.LearningManagement.API.Util;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.SmartCards;

namespace UWP.LearningManagement.ViewModels
{
    public class ModuleViewModel
    {
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


        public Course Course { get; set; }

        public ModuleViewModel() { }
        public ModuleViewModel(int id, int courseId = -1)
        {
            if (id != -1)
            {
                Module = Modules.FirstOrDefault(x => x.Id == id);
            }
            else Module = new Module { Id = -1 };
            if (courseId != -1)
            {
                Course = Courses.FirstOrDefault(x => x.Id == courseId);
            }
        }
        public ModuleViewModel(string name)
        {
            Module = new Module { Name = name };
        }

        public virtual string Display => $"{Module.Name} - {Module.Description}";
        public virtual string AssignDisplay => $"{Module.Name}";

        public async Task<Module> Add()
        {
            if (!Course.Modules.Any(x => x.Name == Module.Name))
            {
                var returnVal = await new WebRequestHandler().Post("http://localhost:5159/Module/AddOrUpdate", Module);
                var deserializedReturn = JsonConvert.DeserializeObject<Module>(returnVal);
                Course.Add(deserializedReturn);
                await new WebRequestHandler().Post("http://localhost:5159/Course/UpdateModules", Course);
                return deserializedReturn;
            }
            return new Module { Name = "Invaliid Name" };
        }

        public async Task Add(Assignment assignment)
        {
            AssignmentItem newAssignment = new AssignmentItem(assignment);
            var returnVal = await new WebRequestHandler().Post("http://localhost:5159/ContentItem/AddOrUpdateAssignmentItem", newAssignment);
            var deserializedReturn = JsonConvert.DeserializeObject<AssignmentItem>(returnVal);
            Module.Add(deserializedReturn);
            await new WebRequestHandler().Post("http://localhost:5159/Module/UpdateItems", Module);
        }
    }
}
