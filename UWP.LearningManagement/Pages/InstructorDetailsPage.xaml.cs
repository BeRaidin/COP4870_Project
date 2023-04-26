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
    public sealed partial class InstructorDetailsPage : Page
    {
        public InstructorDetailsPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is int id)
            {
                DataContext = new InstructorDetailsViewModel(id);
            }
        }

        public void Course_DoubleTapped(object sender, RoutedEventArgs e)
        {
            if ((DataContext as InstructorDetailsViewModel).SelectedCourse != null)
            {
                Frame.Navigate(typeof(CourseDetailsPage), (DataContext as InstructorDetailsViewModel).SelectedCourse.Course.Id);
            }
        }

        private async void AddCourse_Click(object sender, RoutedEventArgs e)
        {
            var addDialog = new CourseDialog((DataContext as InstructorDetailsViewModel).Instructor.Id);
            await addDialog.ShowAsync();
            (DataContext as InstructorDetailsViewModel).Refresh();
        }

        private void JoinCourse_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as InstructorDetailsViewModel).JoinCourse();
        }

        private void Grade_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            (DataContext as InstructorDetailsViewModel).GradeAssignment();

        }

        private async void DropClasses_Click(object sender, RoutedEventArgs e)
        {
            await(DataContext as InstructorDetailsViewModel).DropClasses();

        }

        private void RemovedCourse_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
