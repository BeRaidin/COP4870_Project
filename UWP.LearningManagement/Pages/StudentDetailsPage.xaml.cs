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
using Library.LearningManagement.Models;
using UWP.LearningManagement;

namespace UWP.LearningManagement
{
    public sealed partial class StudentDetailsPage : Page
    {
        public StudentDetailsPage()
        {
            this.InitializeComponent();
            DataContext = new DetailedPersonViewModel();
            gradesFrame.Navigate(typeof(CurrentSemesterPage));
        }

        private void DropClasses_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as DetailedPersonViewModel).DropClasses();
        }

        private void Current_Click(object sender, RoutedEventArgs e)
        {
            gradesFrame.Navigate(typeof(CurrentSemesterPage));
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            gradesFrame.Navigate(typeof(PreviousSemesterPage));
        }
    }
}
