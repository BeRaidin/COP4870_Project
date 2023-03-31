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
using Library.LearningManagement.Model;

namespace UWP.LearningManagement
{
    public sealed partial class DetailedStudent : Page
    {
        public DetailedStudent()
        {
            this.InitializeComponent();
            DataContext = new DetailedPersonViewModel();
        }

        private void Assign_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            (DataContext as DetailedPersonViewModel).SetGrade();
        }

        private void Assign_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var tmp = (GradesDictionary)AssignmentListBox.SelectedItem;
            (DataContext as DetailedPersonViewModel).ChangedSelectedAssignment(tmp.Assignment);
        }
    }
}
