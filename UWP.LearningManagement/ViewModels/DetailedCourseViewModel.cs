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
using Windows.Graphics.Printing;
using Windows.Media.Playback;

namespace UWP.LearningManagement.ViewModels
{
    public class DetailedCourseViewModel
    {
        private readonly CourseService courseService;
        private readonly PersonService personService;
        private readonly ModuleService moduleService;
        private readonly List<Module> allModules;
        private readonly List<Assignment> allAssignments;
        private readonly List<Person> allRoster;
        private readonly List<Announcement> allAnnouncements;

        public Module SelectedModule
        {
            get { return moduleService.CurrentModule; }
            set { moduleService.CurrentModule = value; }
        }
        public Person SelectedPerson
        {
            get { return personService.CurrentPerson; }
            set { personService.CurrentPerson = value; }
        }
        public Course SelectedCourse
        {
            get { return courseService.CurrentCourse; }
            set { courseService.CurrentCourse = value; }
        }
        public Assignment SelectedAssignment { get; set; }
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
        private ObservableCollection<Assignment> _assignments;
        public ObservableCollection<Assignment> Assignments
        {
            get { return _assignments; }
            set { _assignments = value; }
        }
        private ObservableCollection<Person> _roster;
        public ObservableCollection<Person> Roster
        {
            get { return _roster; }
            set { _roster = value; }
        }
        private ObservableCollection<Announcement> _announcements;
        public ObservableCollection<Announcement> Announcements
        {
            get { return _announcements; }
            set { _announcements = value; }
        }
        public string Query { get; set; }
        public string Code
        {
            get { return SelectedCourse.Code; }
        }
        public int Hours
        {
            get { return SelectedCourse.CreditHours; }
        }
        public string Name
        {
            get { return SelectedCourse.Name; }
        }
        public string Room
        {
            get { return SelectedCourse.Room; }
        }

        public DetailedCourseViewModel()
        {
            courseService = CourseService.Current;
            personService = PersonService.Current;
            moduleService = ModuleService.Current;
            allModules = SelectedCourse.Modules;
            allAssignments = SelectedCourse.Assignments;
            allRoster = SelectedCourse.Roster;
            allAnnouncements = SelectedCourse.Announcements;
            Modules = new ObservableCollection<Module>(allModules);
            Assignments = new ObservableCollection<Assignment>(allAssignments);
            Roster = new ObservableCollection<Person>(allRoster);
            Announcements = new ObservableCollection<Announcement>(allAnnouncements);
        }

        public async void AddModule()
        {
            SelectedModule = new Module();
            var dialog = new ModuleDialog();
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            Refresh();
        }

        public async void AddAnnouncement()
        {
            var dialog = new AnnouncementDialog();
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            Refresh();
        }


        public async void EditRoster()
        {
            var dialog = new RosterDialog();
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            Refresh();
        }

        public async void AddAssignment()
        {
            var dialog = new NewAssignmentDialog();
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }

            var assignment = (moduleService.CurrentItem as AssignmentItem).Assignment;
            if (assignment.AssignmentGroup == null)
            {
                var Groupdialog = new AssignGroupDialog(assignment);
                if (Groupdialog != null)
                {
                    await Groupdialog.ShowAsync();
                }
            }

            if (SelectedModule.Name.Equals("Make new Module"))
            {
                var Moduledialog = new ModuleDialog();
                if (Moduledialog != null)
                {
                    await Moduledialog.ShowAsync();
                }
                moduleService.Add();
            }
            Refresh();
            moduleService.CurrentItem = null;
        }

        public void Refresh()
        {
            Modules.Clear();
            foreach (var module in allModules)
            {
                Modules.Add(module);
            }
            Assignments.Clear();
            foreach (var assignment in allAssignments)
            {
                Assignments.Add(assignment);
            }
            Roster.Clear();
            foreach (var person in allRoster)
            {
                Roster.Add(person);
            }
            Announcements.Clear();
            foreach (var announcement in allAnnouncements)
            {
                Announcements.Add(announcement);
            }
        }

        public void DeleteAssignment()
        {
            foreach(var module in Modules)
            {
                foreach(var item in module.Content.ToList())
                {
                    if(item as AssignmentItem != null)
                    {
                        if((item as AssignmentItem).Assignment.Equals(SelectedAssignment))
                        {
                            module.Remove(item);
                        }    
                    }
                }
            }
            Remove(SelectedAssignment);
        }

        public void DeleteModule()
        {
            foreach(var item in SelectedModule.Content.ToList())
            {
                if(item as AssignmentItem != null)
                {
                    Remove((item as AssignmentItem).Assignment);
                }
                SelectedModule.Remove(item);
            }
            SelectedCourse.Modules.Remove(SelectedModule);
        }

        public void Remove(Assignment assignment)
        {
            SelectedCourse.Remove(assignment);
            foreach (var student in SelectedCourse.Roster.ToList())
            {
                if (student as Student != null)
                {
                    (student as Student).Remove(assignment);
                }
            }
        }

        public void EditModule()
        {

        }

        public void EditAssignment()
        {

        }
    }
}
