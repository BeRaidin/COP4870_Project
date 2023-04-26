using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UWP.LearningManagement.Dialogs;
using UWP.LearningManagement.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace UWP.LearningManagement
{
    public sealed partial class EditCoursesPage : Page
    {
        public EditCoursesPage()
        {
            this.InitializeComponent();
            DataContext = new EditCoursesViewModel();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as EditCoursesViewModel).Search();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(InstructorViewPage));
        }

        private async void Remove_Click(object sender, RoutedEventArgs e)
        {
            await (DataContext as EditCoursesViewModel).Delete();
            Frame.Navigate(typeof(InstructorViewPage));
        }

        private async void Edit_Click(object sender, RoutedEventArgs e)
        {
            if ((DataContext as EditCoursesViewModel).SelectedCourse != null)
            {
                var addDialog = new CourseDialog(-1, (DataContext as EditCoursesViewModel).SelectedCourse.Course.Id);
                await addDialog.ShowAsync();
                Frame.Navigate(typeof(InstructorViewPage));
            }
        }
    }
}
