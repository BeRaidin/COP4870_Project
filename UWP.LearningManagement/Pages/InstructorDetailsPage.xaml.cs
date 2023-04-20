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
using Newtonsoft.Json;
using System.Text.RegularExpressions;

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
            Person p = e.Parameter as Person;
            DataContext = new InstructorDetailsViewModel(p);
        }

        public void Course_DoubleTapped(object sender, RoutedEventArgs e)
        {
            if ((DataContext as InstructorDetailsViewModel).SelectedCourse != null)
            {
                Frame.Navigate(typeof(CourseDetailsPage));
            }
        }

        private void AddCourse_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as InstructorDetailsViewModel).AddCourse();
        }

        private void JoinCourse_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as InstructorDetailsViewModel).JoinCourse();
        }

        private void Grade_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            (DataContext as InstructorDetailsViewModel).GradeAssignment();
        }
    }
}
