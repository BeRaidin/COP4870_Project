using UWP.LearningManagement.Dialogs;
using UWP.LearningManagement.ViewModels;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace UWP.LearningManagement
{
    public sealed partial class CourseDetailsPage : Page
    {
        public CourseDetailsPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is int courseId)
            {
                DataContext = new CourseDetailsViewModel(courseId);
            }
        }

        private async void AddModule_Click(object sender, RoutedEventArgs e)
        {
            int courseId = (DataContext as CourseDetailsViewModel).Course.Id;
            var addDialog = new ModuleDialog(-1, courseId);
            await addDialog.ShowAsync();
            (DataContext as CourseDetailsViewModel).Refresh();
        }
        private async void AddAnnouncement_Click(object sender, RoutedEventArgs e)
        {
            int courseId = (DataContext as CourseDetailsViewModel).Course.Id;
            var addDialog = new AnnouncementDialog(-1, courseId);
            await addDialog.ShowAsync();
            (DataContext as CourseDetailsViewModel).Refresh();

        }
        private async void AddAssignment_Click(object sender, RoutedEventArgs e)
        {
            int courseId = (DataContext as CourseDetailsViewModel).Course.Id;
            var addDialog = new AssignmentDialog(-1, courseId);
            await addDialog.ShowAsync();
            (DataContext as CourseDetailsViewModel).Refresh();
        }

        private void UpdateRoster_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as CourseDetailsViewModel).UpdateRoster();
        }
        private async void UpdateModule_Click(object sender, RoutedEventArgs e)
        {
            if ((DataContext as CourseDetailsViewModel).SelectedModule != null)
            {
                int courseId = (DataContext as CourseDetailsViewModel).Course.Id;
                int moduleId = (DataContext as CourseDetailsViewModel).SelectedModule.Module.Id;
                var editDialog = new ModuleDialog(moduleId, courseId);
                await editDialog.ShowAsync();
                (DataContext as CourseDetailsViewModel).Refresh();
            }
        }
        private async void UpdateAnnouncement_Click(object sender, RoutedEventArgs e)
        {
            if ((DataContext as CourseDetailsViewModel).SelectedAnnouncement != null)
            {
                int courseId = (DataContext as CourseDetailsViewModel).Course.Id;
                int announcementId = (DataContext as CourseDetailsViewModel).SelectedAnnouncement.Announcement.Id;
                var editDialog = new AnnouncementDialog(announcementId, courseId);
                await editDialog.ShowAsync();
                (DataContext as CourseDetailsViewModel).Refresh();
            }
        }
        private async void UpdateAssignment_Click(object sender, RoutedEventArgs e)
        {
            if ((DataContext as CourseDetailsViewModel).SelectedAssignment != null)
            {
                int courseId = (DataContext as CourseDetailsViewModel).Course.Id;
                int assignmentId = (DataContext as CourseDetailsViewModel).SelectedAssignment.Assignment.Id;
                var editDialog = new AssignmentDialog(assignmentId, courseId);
                await editDialog.ShowAsync();
                (DataContext as CourseDetailsViewModel).Refresh();
            }
        }
        private async void RemoveModule_Click(object sender, RoutedEventArgs e)
        {
            await (DataContext as CourseDetailsViewModel).RemoveModule();
        }
        private async void RemoveAnnouncement_Click(object sender, RoutedEventArgs e)
        {
            await (DataContext as CourseDetailsViewModel).RemoveAnnouncement();
        }
        private async void RemoveAssignment_Click(object sender, RoutedEventArgs e)
        {
            await (DataContext as CourseDetailsViewModel).RemoveAssignment();
        }

        private void Module_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if ((DataContext as CourseDetailsViewModel).SelectedModule != null)
            {
                int moduleId = (DataContext as CourseDetailsViewModel).SelectedModule.Module.Id;
                Frame.Navigate(typeof(ModuleContentPage), moduleId);
            }
        }
        private void Announcement_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            (DataContext as CourseDetailsViewModel).ViewAnnouncement();
        }
    }
}
