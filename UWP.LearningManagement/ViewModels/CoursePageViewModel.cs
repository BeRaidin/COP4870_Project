﻿using Library.LearningManagement.Services;
using Library.LearningManagement.Models;
using UWP.LearningManagement.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.UI.Xaml.Controls;
using Windows.Foundation.Collections;
using System.Reflection.Metadata;

namespace UWP.LearningManagement.ViewModels
{
    public class CoursePageViewModel
    {
        private readonly CourseService courseService;
        private readonly List<Course> allCourses;

        public Course SelectedCourse
        {
            get { return courseService.CurrentCourse; }
            set { courseService.CurrentCourse = value; }
        }

        private ObservableCollection<Course> _courses;
        public ObservableCollection<Course> Courses
        {
            get
            {
                return _courses;
            }
            private set
            {
                _courses = value;
            }
        }
        public string Query { get; set; }

        public CoursePageViewModel()
        {
            courseService = CourseService.Current;
            allCourses = courseService.CourseList;
            Courses = new ObservableCollection<Course>(allCourses);
        }

        public async void Add()
        {
            SelectedCourse = new Course();
            var dialog = new CourseDialog();
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            Refresh();
        }

        public void Remove()
        {
            if (SelectedCourse != null)
            {
                courseService.Remove();
                Refresh();
            }
        }

        public async void Edit()
        {
            if (SelectedCourse != null)
            {
                var dialog = new EditCourseDialog();
                if (dialog != null)
                {
                    await dialog.ShowAsync();
                }
            }
            Refresh();
        }

        public void Search()
        {
            if (Query != null)
            {
                var searchResults = 
                    allCourses.Where(i => i.Code.Contains(Query, StringComparison.InvariantCultureIgnoreCase) 
                    || i.Name.Contains(Query, StringComparison.InvariantCultureIgnoreCase));
                Courses.Clear();
                foreach (var course in searchResults)
                {
                    Courses.Add(course);
                }
            }
            else
            {
                Refresh();
            }
        }

        public void Refresh()
        {
            Courses.Clear();
            foreach (var course in allCourses)
            {
                Courses.Add(course);
            }
        }
    }
}
