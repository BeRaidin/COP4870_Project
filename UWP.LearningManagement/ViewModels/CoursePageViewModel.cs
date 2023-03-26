using Library.LearningManagement.Services;
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

namespace UWP.LearningManagement.ViewModels
{
    public class CoursePageViewModel
    {
        private CourseService courseService;
        private List<Course> allCourses;
        private ObservableCollection<Course> courses;
        public string Query { get; set; }
        public Course SelectedCourse { get; set; }
        public ObservableCollection<Course> Courses 
        {
            get
            {
                return courses;
            } 
            private set
            {
                courses = value;
            }
        }

        public CoursePageViewModel() 
        { 
            courseService = CourseService.Current;
            allCourses = courseService.Courses;
            courses = new ObservableCollection<Course>(courseService.Courses);
        }

        public async void Add()
        {
            var dialog = new CourseDialog(allCourses);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
        }

        public void Search()
        {
            if (Query != null)
            {
                var searchResults = allCourses.Where(i => i.Code.Contains(Query) || i.Name.Contains(Query));
                Courses.Clear();
                foreach (var course in searchResults)
                {
                    Courses.Add(course);
                }
            }
            else
            {
                Courses.Clear();
                foreach(var course in allCourses)
                {
                    Courses.Add(course);
                }
            }
        }

        public void Remove()
        {
            allCourses.Remove(SelectedCourse);
        }

        public async void Edit()
        {
            if (SelectedCourse != null)
            {
                var dialog = new EditCourseDialog(allCourses, SelectedCourse);
                if (dialog != null)
                {
                    await dialog.ShowAsync();
                }
            }
        }
    }
}
