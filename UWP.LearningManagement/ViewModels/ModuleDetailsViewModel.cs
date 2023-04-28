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
        public IEnumerable<Module> Modules
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Module").Result;
                return JsonConvert.DeserializeObject<List<Module>>(payload);
            }
        }
        private ListNavigator<ContentItemViewModel> ItemListNav { get; set; }
        private List<ContentItemViewModel> ModuleItems { get; set; }

        public Module Module { get; set; }
        public Course SelectedCourse { get; set; }
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
            Module = Modules.FirstOrDefault(x => x.Id == Id);

            ContentItems = new ObservableCollection<ContentItemViewModel>();
            ModuleItems = new List<ContentItemViewModel>();
            foreach (var item in Module.Content)
            {
                ModuleItems.Add(new ContentItemViewModel(item.Id, Module.Id));
            }
            ItemListNav = new ListNavigator<ContentItemViewModel>(ModuleItems);
            Refresh();
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

 

        public void Search()
        {
            if (Query != null && Query != "")
            {

                var searchResults = ContentItems.Where(i => i.ContentItem.Name.Contains(Query, StringComparison.InvariantCultureIgnoreCase));

                ItemListNav = new ListNavigator<ContentItemViewModel>(searchResults.ToList());
                ContentItems.Clear();
                if (ItemListNav.State.Count > 0)
                {
                    foreach (var item in ItemListNav.GetCurrentPage())
                    {
                        ContentItems.Add(item.Value);
                    }
                }



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
            ModuleItems.Clear();
            foreach (var item in Module.Content)
            {
                ModuleItems.Add(new ContentItemViewModel(item.Id, Module.Id));
            }
            ItemListNav = new ListNavigator<ContentItemViewModel>(ModuleItems);
            ContentItems.Clear();
            if (ItemListNav.State.Count > 0)
            {
                foreach (var item in ItemListNav.GetCurrentPage())
                {
                    ContentItems.Add(item.Value);
                }
            }
        }

        public void NextPage()
        {
            if (ItemListNav.HasNextPage)
            {
                ItemListNav.GoForward();
                ContentItems.Clear();
                if (ItemListNav.State.Count > 0)
                {
                    foreach (var item in ItemListNav.GetCurrentPage())
                    {
                        ContentItems.Add(item.Value);
                    }
                }
            }
        }

        public void PreviousPage()
        {
            if (ItemListNav.HasPreviousPage)
            {
                ItemListNav.GoBackward();
                ContentItems.Clear();
                if (ItemListNav.State.Count > 0)
                {
                    foreach (var item in ItemListNav.GetCurrentPage())
                    {
                        ContentItems.Add(item.Value);
                    }
                }
            }
        }
    }
}
