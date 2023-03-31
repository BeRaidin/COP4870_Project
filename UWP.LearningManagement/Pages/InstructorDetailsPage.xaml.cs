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

namespace UWP.LearningManagement
{
    public sealed partial class InstructorDetailsPage : Page
    {
        public InstructorDetailsPage()
        {
            this.InitializeComponent();
            DataContext = new DetailedPersonViewModel();
        }

        public void Course_DoubleTapped(object sender, RoutedEventArgs e)
        {
            if ((DataContext as DetailedPersonViewModel).SelectedCourse != null)
            {
                Frame.Navigate(typeof(DetailedCourse));
            }
        }
    }
}
