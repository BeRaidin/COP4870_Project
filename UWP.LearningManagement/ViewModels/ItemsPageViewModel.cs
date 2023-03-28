using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using UWP.LearningManagement.Dialogs;

namespace UWP.LearningManagement.ViewModels
{
    public class ItemsPageViewModel
    {
        private readonly ModuleService moduleService;
        private readonly List<ContentItem> allItems;
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
        public ContentItem SelectedItem { get; set; }
        public string Query { get; set; }

        public ItemsPageViewModel() 
        {
            moduleService = ModuleService.Current;
            allItems = moduleService.CurrentModule.Content;
            ContentItems = new ObservableCollection<ContentItem>(allItems);
        }

        public async void Add()
        {
            var dialog = new ContentItemDialog();
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            Refresh();
        }

        public void Remove()
        {
            if (SelectedItem != null)
            {
                UpdateCurrentItem();
                moduleService.CurrentModule.Content.Remove(SelectedItem);
                Refresh();
            }
        }

        public async void Edit()
        {
            if (SelectedItem != null)
            {
                UpdateCurrentItem();
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
            if (Query != null)
            {
                var searchResults = allItems.Where(i => i.Name.Contains(Query));
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

        public void UpdateCurrentItem()
        {
            moduleService.CurrentItem = SelectedItem;
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
