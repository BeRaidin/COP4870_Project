using UWP.LearningManagement.ViewModels;
using Windows.UI.Xaml.Controls;

namespace UWP.LearningManagement
{
    public sealed partial class PreviousSemesterPage : Page
    {
        public PreviousSemesterPage()
        {
            this.InitializeComponent();
            DataContext = new PreviousSemesterViewModel();
        }

        private void previousSemester_DoubleTapped(object sender, Windows.UI.Xaml.Input.DoubleTappedRoutedEventArgs e)
        {
            (DataContext as PreviousSemesterViewModel).ViewPastSemester();
        }
    }
}
