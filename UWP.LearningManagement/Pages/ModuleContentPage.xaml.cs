using System;
using UWP.LearningManagement.Dialogs;
using UWP.LearningManagement.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace UWP.LearningManagement
{
    public sealed partial class ModuleContentPage : Page
    {
        public ModuleContentPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is int moduleId)
            {
                DataContext = new ModuleDetailsViewModel(moduleId);
            }
        }

        private async void Add_Click(object sender, RoutedEventArgs e)
        {
            int moduleId = (DataContext as ModuleDetailsViewModel).Module.Id;
            var addDialog = new ContentItemDialog(-1, moduleId);
            await addDialog.ShowAsync();
            (DataContext as ModuleDetailsViewModel).Refresh();
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            await (DataContext as ModuleDetailsViewModel).Remove();
        }

        private async void Edit_Click(object sender, RoutedEventArgs e)
        {
            if ((DataContext as ModuleDetailsViewModel).Module != null)
            {
                int moduleId = (DataContext as ModuleDetailsViewModel).Module.Id;
                int itemId = (DataContext as ModuleDetailsViewModel).Item.ContentItem.Id;
                var editDialog = new ContentItemDialog(itemId, moduleId);
                await editDialog.ShowAsync();
                (DataContext as ModuleDetailsViewModel).Refresh();
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as ModuleDetailsViewModel).Search();
        }
    }
}
