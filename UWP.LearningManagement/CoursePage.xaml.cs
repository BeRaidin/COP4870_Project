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
using Library.LearningManagement.Services;
using Library.LearningManagement.Models;

namespace UWP.LearningManagement
{
    public sealed partial class CoursePage : Page
    {
        public CoursePage()
        {
            this.InitializeComponent();
            DataContext = new CourseViewModel();
        }

        private async void AddNew_Click(object sender, RoutedEventArgs e)
        {       
            var diag = new CourseDialog((DataContext as CourseViewModel).Courses);
            await diag.ShowAsync();           
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as CourseViewModel).Remove();
        }
    }
}
