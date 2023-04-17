using UWP.Library.LearningManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        private ContentItem _currentItem;
        public ContentItem CurrentItem
        {
            get { return _currentItem; }
            set { _currentItem = value; }
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
            CurrentModule = new Module();
            CurrentItem = new ContentItem();
        }

        public void Add(Module module)
        {
            ModuleList.Add(module);
        }

        public void Remove()
        {
            ModuleList.Remove(CurrentModule);
        }

        public void RemoveCurrentItem()
        {
            CurrentModule.Remove(CurrentItem);
        }

        public void Add()
        {
            CurrentModule.Add(CurrentItem);
        }
    }
}
