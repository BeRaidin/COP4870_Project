using UWP.Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UWP.LearningManagement.Dialogs;
using Newtonsoft.Json;
using UWP.LearningManagement.API.Util;
using System.Threading.Tasks;

namespace UWP.LearningManagement.ViewModels
{
    public class ModuleDetailsViewModel
    {
        private readonly ModuleService moduleService;
        private readonly CourseService courseService;
        public IEnumerable<Module> Modules
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Module").Result;
                return JsonConvert.DeserializeObject<List<Module>>(payload);
            }
        }

        public Module Module
        {
            get { return moduleService.CurrentModule; }
            set { moduleService.CurrentModule = value; }
        }
        public Course SelectedCourse
        {
            get { return courseService.CurrentCourse; }
            set { courseService.CurrentCourse = value; }
        }
        public ContentItemViewModel Item { get; set; }

        public ObservableCollection<ContentItemViewModel> ContentItems { get; set; }
        public string Query { get; set; }
        
        public string Course
        {
            get { return SelectedCourse.Code; }
        }
        public int Id { get; set; }
        public ModuleDetailsViewModel() { }

        public ModuleDetailsViewModel(int id)
        {
            Id = id;
            moduleService = ModuleService.Current;
            courseService = CourseService.Current;
            Module = Modules.FirstOrDefault(x => x.Id == Id);
            ContentItems = new ObservableCollection<ContentItemViewModel>();
            foreach (var item in Module.Content)
            {
                ContentItems.Add(new ContentItemViewModel(item.Id, Module.Id));
            }
        }

        public void Add()
        {
            //SelectedItem = new ContentItem();
            //var dialog = new ContentItemDialog();
            //if (dialog != null)
            //{
            //    await dialog.ShowAsync();
            //}
            //
            //if(SelectedItem as AssignmentItem != null)
            //{
            //var assignDialog = new AssignmentDialog();
            //if (assignDialog != null)
            //{
            //    await assignDialog.ShowAsync();
            //}
            //
            //var assignment = (SelectedItem as AssignmentItem).Assignment;
            //if (assignment.AssignmentGroup == null)
            //{
            //    var Groupdialog = new AssignGroupDialog(assignment);
            //    if (Groupdialog != null)
            //    {
            //        await Groupdialog.ShowAsync();
            //    }
            //}
            // }
            // Refresh();
        }

        public async Task Remove()
        {
            if (Item != null)
            {
                Module.Remove(Item.ContentItem);
                await new WebRequestHandler().Post("http://localhost:5159/Module/UpdateItems", Module);
                await new WebRequestHandler().Post("http://localhost:5159/ContentItem/Delete", Item.ContentItem);
                Refresh();
            }
        }

        public async void Edit()
        {
            if (Item != null)
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

                var searchResults = ContentItems.Where(i => i.ContentItem.Name.Contains(Query, StringComparison.InvariantCultureIgnoreCase));

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
            Module = Modules.FirstOrDefault(x => x.Id == Id);
            ContentItems.Clear();
            foreach (var item in Module.Content)
            {
                ContentItems.Add(new ContentItemViewModel(item.Id, Module.Id));
            }
        }
    }
}
