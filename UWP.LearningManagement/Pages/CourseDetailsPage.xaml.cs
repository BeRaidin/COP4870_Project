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

            if (e.Parameter is Course course)
            {
                DataContext = new CourseDetailsViewModel(course);
            }
        }

        private void Module_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as CourseDetailsViewModel).AddModule();
        }

        private void Module_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if ((DataContext as CourseDetailsViewModel).SelectedModule != null)
            {
                Frame.Navigate(typeof(ModuleContentPage));
            }
        }

        private async void AddAnnouncement_Click(object sender, RoutedEventArgs e)
        {
            int courseId = (DataContext as CourseDetailsViewModel).SelectedCourse.Id;
            var addDialog = new AnnouncementDialog(-1, courseId);
            await addDialog.ShowAsync();
        }

        private void Roster_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as CourseDetailsViewModel).EditRoster();
        }

        private void Assignment_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as CourseDetailsViewModel).AddAssignment();
        }

        private void EditModule_Click(object sender, RoutedEventArgs e)
        {
            if ((DataContext as CourseDetailsViewModel).SelectedModule != null)
            {
                (DataContext as CourseDetailsViewModel).EditModule();
            }
        }

        private void DeleteModule_Click(object sender, RoutedEventArgs e)
        {
            if ((DataContext as CourseDetailsViewModel).SelectedModule != null)
            {
                (DataContext as CourseDetailsViewModel).DeleteModule();
            }
        }

        private void EditAssignment_Click(object sender, RoutedEventArgs e)
        {
            if ((DataContext as CourseDetailsViewModel).SelectedAssignment != null)
            {
                (DataContext as CourseDetailsViewModel).EditAssignment();
            }
        }

        private void DeleteAssignment_Click(object sender, RoutedEventArgs e)
        {
            if ((DataContext as CourseDetailsViewModel).SelectedAssignment != null)
            {
                (DataContext as CourseDetailsViewModel).DeleteAssignment();
            }
        }

        private void AnnouncementEdit_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as CourseDetailsViewModel).UpdateAnnouncement();
        }

        private async void AnnouncementDelete_Click(object sender, RoutedEventArgs e)
        {
            await (DataContext as CourseDetailsViewModel).DeleteAnnouncement();
        }

        private void Announcement_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            (DataContext as CourseDetailsViewModel).ViewAnnouncement();
        }
    }
}
