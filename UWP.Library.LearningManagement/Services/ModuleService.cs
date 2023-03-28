using Library.LearningManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Services
{
    public class ModuleService
    {
        private List<Module> moduleList;
        public List<Module> ModuleList
        {
            get
            {
                return moduleList;
            }
            set
            { 
                moduleList = value; 
            }
        }
        private Module _currentModule;
        public Module CurrentModule
        {
            get { return _currentModule; }
            set { _currentModule = value; }
        }
        public static ModuleService Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new ModuleService();
                }

                return instance;
            }
        }
        private static ModuleService instance;

        public ModuleService()
        {
            ModuleList = new List<Module>();
        }

        public void Add(Module module)
        {
            ModuleList.Add(module);
        }

        public void Remove()
        {
            ModuleList.Remove(CurrentModule);
        }
    }
}
