using UWP.LearningManagement.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UWP.LearningManagement
{
    public sealed partial class ModuleContentPage : Page
    {
        public ModuleContentPage()
        {
            this.InitializeComponent();
            DataContext = new ModuleContentViewModel();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as ModuleContentViewModel).Add();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as ModuleContentViewModel).Remove();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as ModuleContentViewModel).Edit();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as ModuleContentViewModel).Search();
        }
    }
}
