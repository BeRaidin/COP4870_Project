using UWP.LearningManagement.Dialogs;
using UWP.LearningManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Xml.Linq;
using UWP.Library.LearningManagement.Models;
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
        private void UpdateModule_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as CourseDetailsViewModel).UpdateModule();
        }
        private void UpdateAnnouncement_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as CourseDetailsViewModel).UpdateAnnouncement();
        }
        private void UpdateAssignment_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as CourseDetailsViewModel).UpdateAssignment();
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
                Frame.Navigate(typeof(ModuleContentPage));
            }
        }
        private void Announcement_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            (DataContext as CourseDetailsViewModel).ViewAnnouncement();
        }
    }
}
