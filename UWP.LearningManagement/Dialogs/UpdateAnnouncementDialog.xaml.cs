using UWP.LearningManagement.ViewModels;
using Windows.UI.Xaml.Controls;

namespace UWP.LearningManagement.Dialogs
{
    public sealed partial class UpdateAnnouncementDialog : ContentDialog
    {
        public UpdateAnnouncementDialog()
        {
            this.InitializeComponent();
            DataContext = new AnnouncementViewModel();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            (DataContext as AnnouncementViewModel).Edit();
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
