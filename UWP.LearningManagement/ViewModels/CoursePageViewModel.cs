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
using System.Reflection.Metadata;

namespace UWP.LearningManagement.ViewModels
{
    public class CoursePageViewModel
    {
        private readonly CourseService courseService;
        private readonly SemesterService semesterService;
        private readonly ListNavigator<Course> allCourses;
        private bool isSearch;

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
        public ListNavigator<Course> SearchResults { get; set; }

        public CoursePageViewModel()
        {
            courseService = CourseService.Current;
            semesterService = SemesterService.Current;
            allCourses = new ListNavigator<Course>(semesterService.CurrentSemester.Courses);
            Courses = new ObservableCollection<Course>(allCourses.PrintPage(allCourses.GoToFirstPage()));
            isSearch = false;
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
                isSearch = true;
                var search =
                    allCourses.State.Where(i => i.Code.Contains(Query, StringComparison.InvariantCultureIgnoreCase)
                    || i.Name.Contains(Query, StringComparison.InvariantCultureIgnoreCase));
                SearchResults = new ListNavigator<Course>(search.ToList());
                Courses.Clear();
                foreach (var course in SearchResults.PrintPage(SearchResults.GoToFirstPage()))
                {
                    Courses.Add(course);
                }
            }
            else
            {
                Refresh();
                isSearch = false;
            }
        }

        public void Refresh()
        {
            Courses.Clear();
            foreach (var course in allCourses.PrintPage(allCourses.GoToFirstPage()))
            {
                Courses.Add(course);
            }
        }

        public void LeftClick()
        {
            if (isSearch && SearchResults.HasPreviousPage)
            {
                Courses.Clear();
                foreach (var course in SearchResults.PrintPage(SearchResults.GoBackward()))
                {
                    Courses.Add(course);
                }
            }
            else if (!isSearch && allCourses.HasPreviousPage)
            {
                Courses.Clear();
                foreach (var course in allCourses.PrintPage(allCourses.GoBackward()))
                {
                    Courses.Add(course);
                }
            }
        }

        public void RightClick()
        {
            if (isSearch && SearchResults.HasNextPage)
            {
                Courses.Clear();
                foreach (var course in SearchResults.PrintPage(SearchResults.GoForward()))
                {
                    Courses.Add(course);
                }
            }
            else if (!isSearch && allCourses.HasNextPage)
            {
                Courses.Clear();
                foreach (var course in allCourses.PrintPage(allCourses.GoForward()))
                {
                    Courses.Add(course);
                }
            }
        }
    }
}
