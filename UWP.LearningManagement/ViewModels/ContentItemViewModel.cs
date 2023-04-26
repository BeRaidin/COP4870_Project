using Library.LearningManagement.Services;
using UWP.Library.LearningManagement.Models;
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
using Newtonsoft.Json;
using UWP.LearningManagement.API.Util;

namespace UWP.LearningManagement.ViewModels
{
    public class ContentItemViewModel
    {
        private readonly ModuleService moduleService;
        public IEnumerable<AssignmentItem> AssignmentItems
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/ContentItem/GetAssignmentItems").Result;
                return JsonConvert.DeserializeObject<List<AssignmentItem>>(payload);
            }
        }
        public IEnumerable<FileItem> FileItems
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/ContentItem/GetFileItems").Result;
                return JsonConvert.DeserializeObject<List<FileItem>>(payload);
            }
        }
        public IEnumerable<PageItem> PageItems
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/ContentItem/GetPageItems").Result;
                return JsonConvert.DeserializeObject<List<PageItem>>(payload);
            }
        }
        public IEnumerable<Module> Modules
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Module").Result;
                return JsonConvert.DeserializeObject<List<Module>>(payload);
            }
        }

        public ContentItem ContentItem { get; set; }

        public ObservableCollection<string> ContentTypes
        {
            get
            {
                return new ObservableCollection<string>
                { "Assignment", "Page", "File" };
            }
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SelectedType { get; set; }
        public Module Module { get; set; }
        public virtual string Display => $"{ContentItem.Name} - {SelectedType}";

        public ContentItemViewModel() { }
        public ContentItemViewModel(int id, int moduleId = -1)
        {
            moduleService = ModuleService.Current;
            if (id != -1)
            {
                ContentItem = AssignmentItems.FirstOrDefault(x => x.Id == id);
                if (ContentItem == null)
                {
                    ContentItem = FileItems.FirstOrDefault(x => x.Id == id);
                    if (ContentItem == null)
                    {
                        ContentItem = PageItems.FirstOrDefault(x => x.Id == id);
                        SelectedType = "Page";
                    }
                    else SelectedType = "File";
                }
                else SelectedType = "Assignment";
            }
            else
            {
                ContentItem = new ContentItem { Id = -1 };
            }
            Module = Modules.FirstOrDefault(x => x.Id == moduleId);
        }

        

        public async Task<ContentItem> AddItem()
        {
            string returnVal;
            ContentItem deserializedReturn;
            if (SelectedType == "Assignment")
            {
                returnVal = await new WebRequestHandler().Post("http://localhost:5159/ContentItem/AddOrUpdateAssignmentItem", ContentItem);
                deserializedReturn = JsonConvert.DeserializeObject<AssignmentItem>(returnVal);
            }
            else if (SelectedType == "File")
            {
                returnVal = await new WebRequestHandler().Post("http://localhost:5159/ContentItem/AddOrUpdateFileItem", ContentItem);
                deserializedReturn = JsonConvert.DeserializeObject<FileItem>(returnVal);
            }
            else
            {
                returnVal = await new WebRequestHandler().Post("http://localhost:5159/ContentItem/AddOrUpdatePageItem", ContentItem);
                deserializedReturn = JsonConvert.DeserializeObject<PageItem>(returnVal);
            }

            Module.Add(deserializedReturn);
            await new WebRequestHandler().Post("http://localhost:5159/Module/UpdateItems", Module);
            return deserializedReturn;
        }

        public void Edit()
        {
            if (Name != null && Name != ""
                && Description != null && Description != null)
            {
                ContentItem.Name = Name;
                ContentItem.Description = Description;
            }
        }
    }
}
