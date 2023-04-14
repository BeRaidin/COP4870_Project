using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UWP.LearningManagement.Dialogs;

namespace UWP.LearningManagement.ViewModels
{
    public class ModuleContentViewModel
    {
        private readonly ModuleService moduleService;
        private readonly CourseService courseService;
        private readonly List<ContentItem> allItems;

        public Module SelectedModule
        {
            get { return moduleService.CurrentModule; }
            set { moduleService.CurrentModule = value; }
        }
        public Course SelectedCourse
        {
            get { return courseService.CurrentCourse; }
            set { courseService.CurrentCourse = value; }
        }
        public ContentItem SelectedItem 
        {
            get { return moduleService.CurrentItem; }
            set { moduleService.CurrentItem = value; }
        }

        private ObservableCollection<ContentItem> _contentItems;
        public ObservableCollection<ContentItem> ContentItems
        {
            get
            {
                return _contentItems;
            }
            private set
            {
                _contentItems = value;
            }
        }
        public string Query { get; set; }
        public string Name
        {
            get { return SelectedCourse.Name; }
        }
        public string Course
        {
            get { return SelectedCourse.Code; }
        }

        public ModuleContentViewModel() 
        {
            moduleService = ModuleService.Current;
            courseService = CourseService.Current;
            allItems = SelectedModule.Content;
            ContentItems = new ObservableCollection<ContentItem>(allItems);
        }

        public async void Add()
        {
            SelectedItem = new ContentItem();
            var dialog = new ContentItemDialog();
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }

            if(SelectedItem as AssignmentItem != null)
            {
                var assignDialog = new AssignmentDialog();
                if (assignDialog != null)
                {
                    await assignDialog.ShowAsync();
                }

                var assignment = (SelectedItem as AssignmentItem).Assignment;
                if (assignment.AssignmentGroup == null)
                {
                    var Groupdialog = new AssignGroupDialog(assignment);
                    if (Groupdialog != null)
                    {
                        await Groupdialog.ShowAsync();
                    }
                }
            }
            Refresh();
        }

        public void Remove()
        {
            if (SelectedItem != null)
            {
                moduleService.RemoveCurrentItem();
                if(SelectedItem is AssignmentItem)
                {
                    SelectedCourse.Remove((SelectedItem as AssignmentItem).Assignment);
                }
                Refresh();
            }
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
            Refresh();
        }

        public void Search()
        {
            if (Query != null && Query != "")
            {
                var searchResults = allItems.Where(i => i.Name.Contains(Query, StringComparison.InvariantCultureIgnoreCase) 
                                                    || i.Id.Contains(Query, StringComparison.InvariantCultureIgnoreCase));

                ContentItems.Clear();
                foreach (var item in searchResults)
                {
                    ContentItems.Add(item);
                }
            }
            else
            {
                Refresh();
            }
        }

        public void Refresh()
        {
            ContentItems.Clear();
            foreach (var item in allItems)
            {
                ContentItems.Add(item);
            }
        }
    }
}
