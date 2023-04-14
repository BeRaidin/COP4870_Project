using UWP.LearningManagement.ViewModels;
using Windows.UI.Xaml.Controls;

namespace UWP.LearningManagement.Dialogs
{
    public sealed partial class CourseDialog : ContentDialog
    {
        public CourseDialog()
        {
            InitializeComponent();
            DataContext = new CourseViewModel();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            (DataContext as CourseViewModel).Add();
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        public bool TestValid()
        {
            return (DataContext as CourseViewModel).IsValid;
        }
    }
}