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
    public sealed partial class CoursePage : Page
    {
        public CoursePage()
        {
            this.InitializeComponent();
            DataContext = new CoursePageViewModel();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as CoursePageViewModel).Add();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as CoursePageViewModel).Remove();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as CoursePageViewModel).Edit();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as CoursePageViewModel).Search();
        }

        private void ListBox_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            (DataContext as CoursePageViewModel).UpdateCurrentCourse();
            Frame.Navigate(typeof(DetailedCourse));
        }
    }
}
