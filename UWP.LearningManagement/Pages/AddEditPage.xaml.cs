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
    public sealed partial class AddEditPage : Page
    {
        public AddEditPage()
        {
            this.InitializeComponent();
            DataContext = new AddEditPageViewModel();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as AddEditPageViewModel).Search();
        }

        private void PersonList_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            (DataContext as AddEditPageViewModel).UpdatePerson();

        }

        private void CourseList_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            (DataContext as AddEditPageViewModel).UpdateCourse();

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as AddEditPageViewModel).Add();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as AddEditPageViewModel).Delete();
        }
    }
}
