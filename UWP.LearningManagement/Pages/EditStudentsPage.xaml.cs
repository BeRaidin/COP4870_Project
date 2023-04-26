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
    public sealed partial class EditStudentsPage : Page
    {
        public EditStudentsPage()
        {
            this.InitializeComponent();
            DataContext = new EditStudentsViewModel();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as EditStudentsViewModel).Search();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(InstructorViewPage));
        }

        private async void Remove_Click(object sender, RoutedEventArgs e)
        {
            await (DataContext as EditStudentsViewModel).Delete();
            Frame.Navigate(typeof(InstructorViewPage));
        }

        private async void Edit_Click(object sender, RoutedEventArgs e)
        {
            if ((DataContext as EditStudentsViewModel).SelectedStudent != null)
            {
                var addDialog = new StudentDialog((DataContext as EditStudentsViewModel).SelectedStudent.Student);
                await addDialog.ShowAsync();
                Frame.Navigate(typeof(InstructorViewPage));

            }
        }
    }
}
