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
    public class DetailedCourseViewModel
    {
        private CourseService courseService;
        private PersonService personService;
        private ModuleService moduleService;
        private readonly List<Module> allModules;
        private ObservableCollection<Module> _modules;
        public ObservableCollection<Module> Modules
        {
            get
            {
                return _modules;
            }
            set
            {
                _modules = value;
            }
        }
        private readonly List<Assignment> allAssignments;
        private ObservableCollection<Assignment> _assignments;
        public ObservableCollection<Assignment> Assignments
        {
            get { return _assignments; }
            set { _assignments = value; }
        }
        private readonly List<Person> allRoster;
        private ObservableCollection<Person> _roster;
        public ObservableCollection<Person> Roster
        {
            get { return _roster; }
            set { _roster = value; }
        }
        private readonly List<Announcement> allAnnouncements;
        private ObservableCollection<Announcement> _announcements;
        public ObservableCollection<Announcement> Announcements
        {
            get { return _announcements; }
            set { _announcements = value; }
        }

        public Module SelectedModule { get; set; }
        public string Query { get; set; }
        public string Code
        {
            get { return courseService.CurrentCourse.Code; }
        }
        public int Hours
        {
            get { return courseService.CurrentCourse.CreditHours; }
        }
        public string Name
        {
            get { return courseService.CurrentCourse.Name; }
        }
        public string Room
        {
            get { return courseService.CurrentCourse.Room; }
        }

        public DetailedCourseViewModel()
        {
            courseService = CourseService.Current;
            personService = PersonService.Current;
            moduleService = ModuleService.Current;
            allModules = courseService.CurrentCourse.Modules;
            allAssignments = courseService.CurrentCourse.Assignments;
            allRoster = courseService.CurrentCourse.Roster;
            allAnnouncements = courseService.CurrentCourse.Announcements;
            Modules = new ObservableCollection<Module>(allModules);
            Assignments = new ObservableCollection<Assignment>(allAssignments);
            Roster = new ObservableCollection<Person>(allRoster);
            Announcements = new ObservableCollection<Announcement>(allAnnouncements);
        }

        public async void Add_Module()
        {
            var dialog = new ModuleDialog();
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            Refresh();
        }

        public async void Add_Announcement()
        {
            var dialog = new AnnouncementDialog();
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            Refresh();
        }

        public void UpdateCurrentModule()
        {
            moduleService.CurrentModule = SelectedModule;
        }

        public void Refresh()
        {
            Modules.Clear();
            foreach (var module in allModules)
            {
                Modules.Add(module);
            }
            Assignments.Clear();
            foreach(var assignment in allAssignments)
            {
                Assignments.Add(assignment);
            }
            Roster.Clear();
            foreach (var person in  allRoster)
            {
                Roster.Add(person);
            }
            Announcements.Clear();
            foreach (var announcement in allAnnouncements)
            {
                Announcements.Add(announcement);
            }
        }

    }
}
