using System;
using UWP.LearningManagement.ViewModels;
using Windows.UI.Xaml.Controls;


namespace UWP.LearningManagement.Dialogs
{
    public sealed partial class AnnouncementDialog : ContentDialog
    {
        public AnnouncementDialog(int id, int courseId)
        {
            this.InitializeComponent();
            DataContext = new AnnouncementViewModel(id, courseId);
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var test = await (DataContext as AnnouncementViewModel).Add();
            Console.WriteLine(test.Title);
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        public bool TestValid()
        {
            return (DataContext as AnnouncementViewModel).IsValid;
        }
    }
}
