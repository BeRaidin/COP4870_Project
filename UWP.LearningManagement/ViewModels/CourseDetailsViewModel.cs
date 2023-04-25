using Library.LearningManagement.Services;
using UWP.Library.LearningManagement.Models;
using UWP.LearningManagement.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Popups;
using Newtonsoft.Json;
using UWP.LearningManagement.API.Util;
using Windows.ApplicationModel.UserActivities.Core;
using Windows.UI.WebUI;
using System.Threading.Tasks;

namespace UWP.LearningManagement.ViewModels
{
    public class CourseDetailsViewModel
    {
        private readonly CourseService courseService;
        private readonly PersonService personService;
        private readonly ModuleService moduleService;
        private readonly List<Assignment> allAssignments;


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
        public AnnouncementViewModel SelectedAnnouncement { get; set; }

        public ObservableCollection<Assignment> Assignments { get; set; }
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


        public ModuleViewModel SelectedModule { get; set; }




        public ObservableCollection<StudentViewModel> Roster { get; set; }
        public ObservableCollection<AdminViewModel> Admin { get; set; }
        public ObservableCollection<AnnouncementViewModel> Announcements { get; set; }
        public ObservableCollection<ModuleViewModel> Modules { get; set; }



        public CourseDetailsViewModel(Course course)
        {
            courseService = CourseService.Current;
            personService = PersonService.Current;
            moduleService = ModuleService.Current;
            SelectedCourse = course;
            allAssignments = SelectedCourse.Assignments;
            Roster = new ObservableCollection<StudentViewModel>();
            Admin = new ObservableCollection<AdminViewModel>();
            Announcements = new ObservableCollection<AnnouncementViewModel>();
            Modules = new ObservableCollection<ModuleViewModel>();
            foreach (var person in SelectedCourse.Roster)
            {
                AdminViewModel admin = new AdminViewModel(person.Id);
                if (admin.Person == null)
                {
                    StudentViewModel student = new StudentViewModel(person.Id);
                    Roster.Add(student);

                }
                else
                {
                    Admin.Add(admin);
                }
            }
            foreach (var announcement in SelectedCourse.Announcements)
            {
                Announcements.Add(new AnnouncementViewModel(announcement.Id));
            }
            foreach (var module in SelectedCourse.Modules)
            {
                Modules.Add(new ModuleViewModel(module.Id));
            }



            Assignments = new ObservableCollection<Assignment>(allAssignments);
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
                        //SelectedModule = new Module();
                        //var Moduledialog = new ModuleDialog();
                        //if (Moduledialog != null)
                        //{
                        //    await Moduledialog.ShowAsync();
                        //}
                        //cont = Moduledialog.Test();
                        //if (cont)
                        //{
                        //    moduleService.Add();
                        //}
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
            foreach (var module in SelectedCourse.Modules)
            {
                Modules.Add(new ModuleViewModel(module.Id));
            }
            Assignments.Clear();
            foreach (var assignment in allAssignments)
            {
                Assignments.Add(assignment);
            }


        }



        public void DeleteAssignment()
        {
            //foreach (var module in Modules)
            //{
            //    foreach (var item in module.Content.ToList())
            //    {
            //        if (item as AssignmentItem != null)
            //        {
            //            if ((item as AssignmentItem).Assignment.Equals(SelectedAssignment))
            //            {
            //                module.Remove(item);
            //            }
            //        }
            //    }
            //}
            //Remove(SelectedAssignment);
            //Refresh();
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
                courseService.CurrentAnnouncement = SelectedAnnouncement.Announcement;
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

        public async void GetError()
        {
            var errorDialog = new ErrorDialog();
            if (errorDialog != null)
            {
                await errorDialog.ShowAsync();
            }
        }






        public async Task RemoveAnnouncement()
        {
            if (SelectedAnnouncement != null)
            {
                SelectedCourse.Remove(SelectedAnnouncement.Announcement);
                await new WebRequestHandler().Post("http://localhost:5159/Course/UpdateAnnouncements", SelectedCourse);
                await new WebRequestHandler().Post("http://localhost:5159/Announcement/Delete", SelectedAnnouncement.Announcement);
            }
            Refresh();
        }

        public async Task RemoveModule()
        {
            if (SelectedModule != null)
            {
                SelectedCourse.Remove(SelectedModule.Module);
                await new WebRequestHandler().Post("http://localhost:5159/Course/UpdateAnnouncements", SelectedCourse);
                await new WebRequestHandler().Post("http://localhost:5159/Announcement/Delete", SelectedModule.Module);
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
    }
}
