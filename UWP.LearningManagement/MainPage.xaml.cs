using LearningManagement.ViewModels;
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
            DataContext = new MainPageViewModel();
        }

        private void StudentView_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(StudentViewPage));
        }

        private void InstructorView_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(InstructorViewPage));
        }
        private async void Left_Click(object sender, RoutedEventArgs e)
        {
            await (DataContext as MainPageViewModel).LeftClick();
        }

        private async void Right_Click(object sender, RoutedEventArgs e)
        {
            await (DataContext as MainPageViewModel).RightClick();
        }
    }
}
