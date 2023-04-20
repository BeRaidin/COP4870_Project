using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
            DataContext = new UnenrollStudentsViewModel();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as UnenrollStudentsViewModel).Search();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(InstructorViewPage));
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as UnenrollStudentsViewModel).Delete();
            Frame.Navigate(typeof(InstructorViewPage));
        }


    }
}
