using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using UWP.LearningManagement.Dialogs;

namespace UWP.LearningManagement.ViewModels
{
    public class ItemsPageViewModel
    {
        private readonly ContentItemService contentService;
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
            contentService = ContentItemService.Current;
            allItems = contentService.ContentList;
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
                contentService.Remove();
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

        }

        public void UpdateCurrentItem()
        {
            contentService.CurrentContent = SelectedItem;
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
