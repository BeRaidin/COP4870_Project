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

namespace UWP.LearningManagement
{
    public sealed partial class InstructorViewPage : Page
    {
        public InstructorViewPage()
        {
            this.InitializeComponent();
            DataContext = new InstructorViewViewModel();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as InstructorViewViewModel).Search();
        }

        private void ListBox_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if ((DataContext as InstructorViewViewModel).SelectedInstructor != null)
            {
                frame.Navigate(typeof(InstructorDetailsPage), (DataContext as InstructorViewViewModel).SelectedInstructor);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private async void AddStudent_Click(object sender, RoutedEventArgs e)
        {
            var addDialog = new StudentDialog();
            await addDialog.ShowAsync();
        }

        private void GoToEditStudents_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EditStudentsPage));
        }

        private async void AddAdmin_Click(object sender, RoutedEventArgs e)
        {
            var addDialog = new AdminDialog(DataContext as InstructorViewViewModel);
            await addDialog.ShowAsync();
            (DataContext as InstructorViewViewModel).Refresh();
        }

        private async void EditAdmin_Click(object sender, RoutedEventArgs e)
        {
            if ((DataContext as InstructorViewViewModel).SelectedInstructor != null)
            {
                var addDialog = new AdminDialog(DataContext as InstructorViewViewModel);
                await addDialog.ShowAsync();
                (DataContext as InstructorViewViewModel).Refresh();
            }
        }

        private async void RemoveAdmin_Click(object sender, RoutedEventArgs e)
        {
            if ((DataContext as InstructorViewViewModel).SelectedInstructor != null)
            {
                await (DataContext as InstructorViewViewModel).Delete();
                (DataContext as InstructorViewViewModel).Refresh();
            }
        }
    }
}
