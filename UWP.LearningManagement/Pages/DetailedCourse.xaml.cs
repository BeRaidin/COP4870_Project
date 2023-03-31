﻿using UWP.LearningManagement.Dialogs;
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

namespace UWP.LearningManagement
{
    public sealed partial class DetailedCourse : Page
    {
        public DetailedCourse() 
        {
            this.InitializeComponent();
            DataContext = new DetailedCourseViewModel();
        }

        private void Module_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as DetailedCourseViewModel).AddModule();
        }

        private void Module_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if ((DataContext as DetailedCourseViewModel).SelectedModule != null)
            {
                Frame.Navigate(typeof(ItemsPage));
            }
        }

        private void Announcement_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as DetailedCourseViewModel).AddAnnouncement();
        }

        private void Roster_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as DetailedCourseViewModel).EditRoster();
        }

        private void Student_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if ((DataContext as DetailedCourseViewModel).SelectedPerson as Student != null)
            {
                Frame.Navigate(typeof(DetailedStudent));
            }
        }

        private void Assignment_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as DetailedCourseViewModel).AddAssignment();
        }

        private void EditModule_Click(object sender, RoutedEventArgs e)
        {
            if ((DataContext as DetailedCourseViewModel).SelectedModule != null)
            {
                (DataContext as DetailedCourseViewModel).EditModule();

            }

        }

        private void DeleteModule_Click(object sender, RoutedEventArgs e)
        {
            if ((DataContext as DetailedCourseViewModel).SelectedModule != null)
            {
                (DataContext as DetailedCourseViewModel).DeleteModule();

            }

        }

        private void EditAssignment_Click(object sender, RoutedEventArgs e)
        {
            if ((DataContext as DetailedCourseViewModel).SelectedAssignment != null)
            {
                (DataContext as DetailedCourseViewModel).EditAssignment();
            }
        }

        private void DeleteAssignment_Click(object sender, RoutedEventArgs e)
        {
            if ((DataContext as DetailedCourseViewModel).SelectedAssignment != null)
            {
                (DataContext as DetailedCourseViewModel).DeleteAssignment();
            }
        }
    }
}
