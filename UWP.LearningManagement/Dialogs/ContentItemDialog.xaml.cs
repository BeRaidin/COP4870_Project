using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UWP.LearningManagement.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace UWP.LearningManagement.Dialogs
{
    public sealed partial class ContentItemDialog : ContentDialog
    {
        public ContentItemDialog(int id, int moduleId)
        {
            this.InitializeComponent();
            DataContext = new ContentItemViewModel(id, moduleId);
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var test = await (DataContext as ContentItemViewModel).AddItem();
            Console.WriteLine(test.Name);
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
