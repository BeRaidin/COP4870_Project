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
        private static ModuleService instance;
        private Module currentModule;
        public Module CurrentModule
        {
            get { return currentModule; }
            set { currentModule = value; }
        }

        public ModuleService()
        {
            moduleList = new List<Module>();

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

        public List<Module> Modules
        {
            get
            {
                return moduleList;
            }
        }

        public void Add(Module module)
        {
            moduleList.Add(module);
        }

        public void Remove()
        {
            moduleList.Remove(currentModule);
        }
    }
}
