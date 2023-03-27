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
        private CourseService courseService;
        private PersonService personService;
        private ModuleService moduleService;
        public List<string> ContentTypes { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SelectedType { get; set; }
        private ContentItem contentItem { get; set; }
        public ContentItem ContentItem
        {
            get { return contentItem; }
            set { contentItem = value; }
        }
        public ContentItemViewModel()
        {
            courseService = CourseService.Current;
            personService = PersonService.Current;
            moduleService = ModuleService.Current;
            ContentTypes = new List<string>
            { "Assignment", "Page", "File" };
        }

        public void Set()
        {
            if (SelectedType == "Assignment")
            {
                ContentItem = new AssignmentItem();
            }
            else if(SelectedType == "Page")
            {
                ContentItem = new PageItem();
            }
            else if (SelectedType == "File")
            {
                ContentItem = new FileItem();
            }
            else ContentItem = new ContentItem();
            ContentItem.Name = Name;
            ContentItem.Description = Description;
        }

        public void Add()
        {
            Set();
            moduleService.CurrentModule.Content.Add(ContentItem);
        }

        public void Edit()
        {

        }
    }
}
