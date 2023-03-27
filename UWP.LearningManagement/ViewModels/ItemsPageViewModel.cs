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
    public class ItemsPageViewModel
    {
        private CourseService courseService;
        private PersonService personService;
        private ModuleService moduleService;

        private ObservableCollection<ContentItem> contentItems;
        public ObservableCollection<ContentItem> ContentItems
        {
            get
            {
                return contentItems;
            }
            private set
            {
                contentItems = value;
            }
        }
        public ContentItem SelectedItem { get; set; }

        public string Query { get; set; }

        public ItemsPageViewModel() 
        {
            courseService = CourseService.Current;
            personService = PersonService.Current;
            moduleService = ModuleService.Current;
            ContentItems = new ObservableCollection<ContentItem>(moduleService.CurrentModule.Content);
        }

        public async void Add()
        {
            var dialog = new ContentItemDialog();
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
        }

        public void Remove()
        {
            moduleService.CurrentModule.Content.Remove(SelectedItem);
            ContentItems.Remove(SelectedItem);
        }

        public async void Edit()
        {
            if (SelectedItem != null)
            {
                var dialog = new EditContentItemDialog();
                if (dialog != null)
                {
                    await dialog.ShowAsync();
                }
            }
        }

        public void Search()
        {

        }

        public void UpdateCurrentModule()
        {

        }
    }
}
