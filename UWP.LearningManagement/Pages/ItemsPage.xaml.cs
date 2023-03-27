using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWP.LearningManagement.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace UWP.LearningManagement
{
    public sealed partial class ModulesPage : Page
    {
        public ModulesPage()
        {
            this.InitializeComponent();
            DataContext = new ItemsPageViewModel();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as ItemsPageViewModel).Add();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as ItemsPageViewModel).Remove();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as ItemsPageViewModel).Edit();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as ItemsPageViewModel).Search();
        }

        private void ListBox_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            (DataContext as ItemsPageViewModel).UpdateCurrentModule();
            //Frame.Navigate(typeof(DetailedCourse));
        }
    }
}
