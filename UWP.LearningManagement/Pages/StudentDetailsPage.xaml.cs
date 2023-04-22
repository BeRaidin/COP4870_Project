﻿using UWP.LearningManagement.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace UWP.LearningManagement
{
    public sealed partial class StudentDetailsPage : Page
    {
        public StudentDetailsPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is StudentViewModel selectedPerson)
            {
                DataContext = new StudentDetailsViewModel(selectedPerson);
                gradesFrame.Navigate(typeof(CurrentSemesterPage));
            }
        }

        private async void DropClasses_Click(object sender, RoutedEventArgs e)
        {
            if ((DataContext as StudentDetailsViewModel).CanDrop())
            {
                await (DataContext as StudentDetailsViewModel).DropClasses();
                CurrentSemester();
            }
        }

        private void Current_Click(object sender, RoutedEventArgs e)
        {
            gradesFrame.Navigate(typeof(CurrentSemesterPage));
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            gradesFrame.Navigate(typeof(PreviousSemesterPage));
        }

        private void Unsubmitted_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            (DataContext as StudentDetailsViewModel).SubmitAssignment();
        }

        private void CurrentSemester()
        {
            gradesFrame.Navigate(typeof(CurrentSemesterPage));
        }
    }
}
