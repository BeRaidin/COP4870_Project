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
    public sealed partial class StudentViewPage : Page
    {
        public StudentViewPage()
        {
            this.InitializeComponent();
            DataContext = new StudentViewViewModel();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as StudentViewViewModel).Search();
        }

        private void Student_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if ((DataContext as StudentViewViewModel).SelectedStudent != null)
            {
                frame.Navigate(typeof(StudentDetailsPage), (DataContext as StudentViewViewModel).SelectedStudent.Id);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
