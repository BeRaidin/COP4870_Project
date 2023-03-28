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

namespace UWP.LearningManagement
{
    public sealed partial class PeoplePage : Page
    {
        public PeoplePage()
        {
            this.InitializeComponent();
            DataContext = new PersonPageViewModel();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as PersonPageViewModel).Add();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as PersonPageViewModel).Remove();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as PersonPageViewModel).Edit();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as PersonPageViewModel).Search();
        }

        private void ListBox_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if ((DataContext as PersonPageViewModel).SelectedPerson != null)
            {
                (DataContext as PersonPageViewModel).UpdateCurrentPerson();
                Frame.Navigate(typeof(DetailedPerson));
            }
        }
    }
}
