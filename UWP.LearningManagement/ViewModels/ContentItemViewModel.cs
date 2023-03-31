using Library.LearningManagement.Services;
using Library.LearningManagement.Models;
using UWP.LearningManagement.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.UI.Xaml.Controls;
using Windows.Foundation.Collections;
using System.Reflection.Metadata;

namespace UWP.LearningManagement.ViewModels
{
    public class ContentItemViewModel
    {
        private readonly ModuleService moduleService;

        public Module SelectedModule
        {
            get { return moduleService.CurrentModule; }
            set { moduleService.CurrentModule = value; }
        }
        public ContentItem SelectedContentItem
        {
            get { return moduleService.CurrentItem; }
            set { moduleService.CurrentItem = value; }
        }
        public ObservableCollection<string> ContentTypes { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SelectedType { get; set; }

        public ContentItemViewModel()
        {
            moduleService = ModuleService.Current;
            ContentTypes = new ObservableCollection<string>
            { "Assignment", "Page", "File" };
        }

        public void Set()
        {
            if (SelectedType == "Assignment")
            {
                SelectedContentItem = new AssignmentItem();
            }
            else if(SelectedType == "Page")
            {
                SelectedContentItem = new PageItem();
            }
            else if (SelectedType == "File")
            {
                SelectedContentItem = new FileItem();
            }
            else SelectedContentItem = new PageItem();
            SelectedContentItem.Name = Name;
            SelectedContentItem.Description = Description;
        }

        public void Add()
        {
            Set();
            SelectedModule.Add(SelectedContentItem);
        }

        public void Edit()
        {
            SelectedContentItem.Name = Name;
            SelectedContentItem.Description = Description;
        }
    }
}
