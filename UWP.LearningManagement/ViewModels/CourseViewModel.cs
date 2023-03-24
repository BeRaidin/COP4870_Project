using UWP.LearningManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UWP.LearningManagement.ViewModels
{
    public class CourseViewModel : INotifyPropertyChanged
    {
        public Course SelectedCourse { get; set; }
        public ObservableCollection<Course> Courses { get; set; }

        public CourseViewModel() 
        { 
            Courses = new ObservableCollection<Course>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Remove()
        {
            Courses.Remove(SelectedCourse);
        }
    }
}
