using UWP.LearningManagement.ViewModels;
using UWP.Library.LearningManagement.Models;
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

            if (e.Parameter is int id)
            {
                DataContext = new StudentDetailsViewModel(id);
                NavToCurrentSemester();
            }
        }

        private async void DropClasses_Click(object sender, RoutedEventArgs e)
        {
            await (DataContext as StudentDetailsViewModel).DropClasses();
            NavToCurrentSemester();
        }

        private void Current_Click(object sender, RoutedEventArgs e)
        {
            NavToCurrentSemester();
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            gradesFrame.Navigate(typeof(PreviousSemesterPage));
        }

        private void Unsubmitted_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            (DataContext as StudentDetailsViewModel).SubmitAssignment();
        }

        private void NavToCurrentSemester()
        {
            int studentId = (DataContext as StudentDetailsViewModel).Student.Id;
            gradesFrame.Navigate(typeof(CurrentSemesterPage), studentId);
        }
    }
}
