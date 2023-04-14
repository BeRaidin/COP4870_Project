using Windows.UI.Xaml.Controls;


namespace UWP.LearningManagement.Dialogs
{
    public sealed partial class ErrorDialog : ContentDialog
    {
        public ErrorDialog()
        {
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

       
    }
}
