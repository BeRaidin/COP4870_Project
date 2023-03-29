using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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


namespace UWP.LearningManagement
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void EditPerson_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(typeof(PeoplePage));
        }

        private void EditCourse_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(typeof(CoursePage));
        }

        private void StudentView_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(typeof(StudentViewPage));
        }

        private void InstructorView_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(typeof(InstructorViewPage));
        }
    }
}
