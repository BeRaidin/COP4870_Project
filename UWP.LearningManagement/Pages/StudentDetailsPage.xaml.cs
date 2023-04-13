using UWP.LearningManagement.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace UWP.LearningManagement
{
    public sealed partial class StudentDetailsPage : Page
    {
        public StudentDetailsPage()
        {
            this.InitializeComponent();
            DataContext = new StudentDetailsViewModel();
            gradesFrame.Navigate(typeof(CurrentSemesterPage));
        }

        private void DropClasses_Click(object sender, RoutedEventArgs e)
        {
            if ((DataContext as StudentDetailsViewModel).CanDrop())
            {
                (DataContext as StudentDetailsViewModel).DropClasses();
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
    }
}
