using System;
using UWP.LearningManagement.ViewModels;
using Windows.UI.Xaml.Controls;

namespace UWP.LearningManagement.Dialogs
{
    public sealed partial class CourseDialog : ContentDialog
    {
        public CourseDialog(InstructorDetailsViewModel idvm)
        {
            InitializeComponent();
            if (idvm.CurrentCourse != null)
            {
                DataContext = idvm.CurrentCourse;
            }
            else
            {
                DataContext = new CourseViewModel(idvm);
            }
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var test = await (DataContext as CourseViewModel).AddCourse();
            Console.WriteLine(test.Name);
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