using Library.LearningManagement.Services;
using Library.LearningManagement.Models;
using UWP.LearningManagement.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Popups;

namespace UWP.LearningManagement.ViewModels
{
    public class CourseDetailsViewModel
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
        public Assignment SelectedAssignment
        {
            get { return personService.CurrentAssignment; }
            set { personService.CurrentAssignment = value; }
        }
        public Announcement SelectedAnnouncement { get; set; }

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

        public string Title { get; set; }
        public string Message { get; set; }

        public CourseDetailsViewModel()
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
            if (!dialog.TestValid())
            {
                GetError();
            }
            Refresh();
            SelectedModule = null;
        }

        public async void AddAnnouncement()
        {
            var dialog = new AnnouncementDialog();
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            if (!dialog.TestValid())
            {
                GetError();
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
            bool cont = true;
            var dialog = new NewAssignmentDialog();
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            if (!dialog.TestValid())
            {
                var errorDialog = new ErrorDialog();
                if (errorDialog != null)
                {
                    await errorDialog.ShowAsync();
                }
            }
            else
            {
                var assignment = (moduleService.CurrentItem as AssignmentItem).Assignment;
                if (assignment.AssignmentGroup == null)
                {
                    var Groupdialog = new AssignGroupDialog(assignment);
                    if (Groupdialog != null)
                    {
                        await Groupdialog.ShowAsync();
                    }
                    if (!Groupdialog.TestValid())
                    {
                        var errorDialog = new ErrorDialog();
                        if (errorDialog != null)
                        {
                            await errorDialog.ShowAsync();
                        }
                        cont = false;
                    }
                }

                if (cont)
                {
                    if (SelectedModule.Name.Equals("Make new Module"))
                    {
                        SelectedModule = new Module();
                        var Moduledialog = new ModuleDialog();
                        if (Moduledialog != null)
                        {
                            await Moduledialog.ShowAsync();
                        }
                        cont = Moduledialog.Test();
                        if (cont)
                        {
                            moduleService.Add();
                        }
                    }
                }
                if (!cont)
                {
                    SelectedCourse.Remove(assignment);
                    var errorDialog = new ErrorDialog();
                    if (errorDialog != null)
                    {
                        await errorDialog.ShowAsync();
                    }
                }
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
            Refresh();
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
            Refresh();

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

        public async void EditModule()
        {
            var dialog = new EditModuleDialog();
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            if (!dialog.TestValid())
            {
                GetError();
            }
            Refresh();
        }

        public async void EditAssignment()
        {
            var dialog = new EditAssignmentDialog();
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            if (!dialog.TestValid())
            {
                GetError();
            }
            Refresh();
        }
        
        public async void UpdateAnnouncement()
        {
            if (SelectedAnnouncement != null)
            {
                courseService.CurrentAnnouncement = SelectedAnnouncement;
                var dialog = new UpdateAnnouncementDialog();
                if (dialog != null)
                {
                    await dialog.ShowAsync();
                }
                if (!dialog.TestValid())
                {
                    GetError();
                }
                Refresh();
            }
        }

        public void DeleteAnnouncement()
        {
            if (SelectedAnnouncement != null)
            {
                SelectedCourse.Announcements.Remove(SelectedAnnouncement);
            }
            Refresh();
        }

        public async void ViewAnnouncement()
        {
            var messageDialog = new MessageDialog(SelectedAnnouncement.Message, SelectedAnnouncement.Title);
            messageDialog.Commands.Add(new UICommand("OK"));
            if (messageDialog != null)
            {
                await messageDialog.ShowAsync();
            }
        }

        public async void GetError()
        {    
            var errorDialog = new ErrorDialog();
            if (errorDialog != null)
            {
                await errorDialog.ShowAsync();
            }
        }
    }
}
